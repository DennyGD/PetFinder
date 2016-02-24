namespace PetFinder.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using Contracts;
    using PetFinder.Common.Constants;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;
    using System.IO;
    public class PostsService : IPostsService
    {
        private const int FilesMaxCount = 3;

        private const int MaxFileSizeInKiloBytes = 120000;

        private readonly IDbRepository<Post> postsRepo;
        private readonly IDbRepository<Image> imagesRepo;
        private readonly IDbRepository<Comment> commentsRepo;
        private readonly IRegionsService regionsService;
        private readonly IPostCategoriesService postCategoriesService;
        private readonly IPetsService petsService;
        private readonly IUsersService usersService;

        private List<string> allowedFileExtensions = new List<string>() { "jpg", "jpeg", "png" };

        public PostsService(
            IDbRepository<Post> postsRepo,
            IDbRepository<Image> imagesRepo,
            IDbRepository<Comment> commentsRepo,
            IRegionsService regionsService, 
            IPostCategoriesService postCategoriesService, 
            IPetsService petsService, 
            IUsersService usersService)
        {
            this.postsRepo = postsRepo;
            this.imagesRepo = imagesRepo;
            this.commentsRepo = commentsRepo;
            this.regionsService = regionsService;
            this.postCategoriesService = postCategoriesService;
            this.petsService = petsService;
            this.usersService = usersService;
        }

        public IQueryable<Post> LastByCategory(string category, int count = 5)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return null;
            }

            return this.postsRepo
                .All()
                .Where(x => (x.PostCategory.Name.ToLower() == category.ToLower()) && !x.IsSolved)
                .OrderByDescending(x => x.CreatedOn)
                .Take(count);
        }

        public IQueryable<Post> All(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.postsRepo.AllWithDeleted();
            }
            else
            {
                return this.postsRepo.All();
            }
        }

        public IQueryable<Post> All(bool isSolved, string category = "")
        {
            Expression<Func<Post, bool>> query;
            if (string.IsNullOrWhiteSpace(category))
            {
                query = x => x.IsSolved == isSolved;
            }
            else
            {
                query = x => (x.IsSolved == isSolved) && (x.PostCategory.Name.ToLower() == category.ToLower());
            }

            return this.postsRepo
                .All()
                .Where(query)
                .OrderByDescending(x => x.CreatedOn);
        }

        public IQueryable<Post> All(int page, int pageSize, string region)
        {
            if (region == null || region == Others.AllRegions)
            {
                region = string.Empty;
            }

            var skip = (page - 1) * pageSize;

            return this.postsRepo
                .All()
                .Where(x => x.Region.Name.Contains(region))
                .OrderByDescending(x => x.CreatedOn)
                .ThenBy(x => x.Id)
                .Skip(skip)
                .Take(pageSize);
        }

        public Post ById(int id)
        {
            return this.postsRepo.GetById(id);
        }

        public int AllPostsCount(string region)
        {
            if (region == null || region == Others.AllRegions)
            {
                region = string.Empty;
            }

            return this.postsRepo.All().Where(x => x.Region.Name.Contains(region)).Count();
        }

        // Mordor in code :/
        public Post Add(
            string title, 
            string content, 
            DateTime eventTime, 
            int regionId, 
            int postCategoryId, 
            int petId, 
            string userId, 
            IEnumerable<HttpPostedFileBase> files)
        {
            var dateTimeNow = DateTime.Now;
            if (eventTime > dateTimeNow || eventTime < dateTimeNow.AddYears(-1))
            {
                return null;
            }

            var region = this.regionsService.ById(regionId, false);
            if (region == null)
            {
                return null;
            }

            var postCategory = this.postCategoriesService.ById(postCategoryId, false);
            if (postCategory == null)
            {
                return null;
            }

            var pet = this.petsService.ById(petId, false);
            if (pet == null)
            {
                return null;
            }

            var user = this.usersService.ById(userId, false);
            if (user == null)
            {
                return null;
            }

            var post = new Post()
            {
                Title = title,
                Content = content,
                EventTime = eventTime,
                Region = region,
                PostCategory = postCategory,
                Pet = pet,
                User = user
            };

            int currentFilesCount = 0;
            foreach (var item in files)
            {
                if (currentFilesCount >= FilesMaxCount)
                {
                    break;
                }

                var image = this.GetImage(item);
                if (image != null)
                {
                    this.imagesRepo.Add(image);
                    post.Images.Add(image);
                }

                currentFilesCount++;
            }

            this.postsRepo.Add(post);

            try
            {
                this.postsRepo.Save();
                this.imagesRepo.Save();
                return this.ById(post.Id);
            }
            catch (Exception)
            {
                // log
                return null;
            }
        }

        public void Update(string title, string content, bool isDeleted, int id)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            var postToUpdate = this.postsRepo.AllWithDeleted().Where(x => x.Id == id).FirstOrDefault();
            if (postToUpdate == null)
            {
                return;
            }

            postToUpdate.Title = title;
            postToUpdate.Content = content;
            if (postToUpdate.IsDeleted != isDeleted)
            {
                this.HandleChangesInIsDeletedStatus(postToUpdate, isDeleted);
            }

            try
            {
                this.postsRepo.Save();
            }
            catch (Exception)
            {
                // log
            }
        }

        public void HardDelete(int id)
        {
            var postToDelete = this.postsRepo.AllWithDeleted().Where(x => x.Id == id).FirstOrDefault();
            if (postToDelete == null)
            {
                return;
            }

            foreach (var comment in postToDelete.Comments)
            {
                this.commentsRepo.HardDelete(comment);
            }

            this.commentsRepo.Save();
            this.postsRepo.HardDelete(postToDelete);
            this.postsRepo.Save();
        }

        private Image GetImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return null;
            }

            Image image = null;
            using (var memory = new MemoryStream())
            {
                file.InputStream.CopyTo(memory);
                var content = memory.GetBuffer();

                var fileName = file.FileName;
                var lastDotIndex = fileName.LastIndexOf('.');
                var fileExtension = (fileName.Substring(lastDotIndex + 1)).ToLower();
                var fileSize = file.ContentLength;
                if (fileSize <= MaxFileSizeInKiloBytes && this.allowedFileExtensions.Contains(fileExtension))
                {
                    image = new Image();
                    image.Content = content;
                    image.FileExtension = fileExtension;
                }
            }

            return image;
        }

        private void HandleChangesInIsDeletedStatus(Post post, bool isDeleted)
        {
            if (isDeleted == true)
            {
                this.DeleteComments(post);
                this.postsRepo.Delete(post);
            }
            else
            {
                this.UndoDeleteComments(post);
                post.IsDeleted = false;
            }
        }

        private void DeleteComments(Post post)
        {
            foreach (var comment in post.Comments)
            {
                this.commentsRepo.Delete(comment);
            }

            this.commentsRepo.Save();
        }

        private void UndoDeleteComments(Post post)
        {
            foreach (var comment in post.Comments)
            {
                comment.IsDeleted = false;
            }

            this.commentsRepo.Save();
        }
    }
}
