namespace PetFinder.Services.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Contracts;
    using PetFinder.Common.Constants;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;

    public class PostsService : IPostsService
    {
        private readonly IDbRepository<Post> postsRepo;

        private readonly IRegionsService regionsService;

        private readonly IPostCategoriesService postCategoriesService;

        private readonly IPetsService petsService;

        private readonly IUsersService usersService;

        public PostsService(
            IDbRepository<Post> postsRepo, 
            IRegionsService regionsService, 
            IPostCategoriesService postCategoriesService, 
            IPetsService petsService, 
            IUsersService usersService)
        {
            this.postsRepo = postsRepo;
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

        // this looks awful :/
        public Post Add(string title, string content, DateTime eventTime, int regionId, int postCategoryId, int petId, string userId)
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

            this.postsRepo.Add(post);

            try
            {
                this.postsRepo.Save();
                return this.ById(post.Id);
            }
            catch (Exception)
            {
                // log
                return null;
            }
        }
    }
}
