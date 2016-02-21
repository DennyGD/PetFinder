namespace PetFinder.Services.Data
{
    using System;
    using System.Linq;

    using Contracts;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;
    
    public class CommentsService : ICommentsService
    {
        private readonly IDbRepository<Comment> commentsRepo;

        private readonly IUsersService usersService;

        public CommentsService(IDbRepository<Comment> commentsRepo, IUsersService usersService)
        {
            this.commentsRepo = commentsRepo;
            this.usersService = usersService;
        }

        public IQueryable<Comment> AllByPostId(int postId, int? takeSize)
        {
            if (takeSize == null || takeSize < 1)
            {
                return this.commentsRepo
                .All()
                .Where(x => x.PostId == postId)
                .OrderByDescending(x => x.CreatedOn);
            }

            return this.commentsRepo
                .All()
                .Where(x => x.PostId == postId)
                .OrderByDescending(x => x.CreatedOn)
                .Take((int)takeSize);
        }

        public Comment Add(string content, int postId, string userId)
        {
            if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            var user = this.usersService.ById(userId, false);
            if (user == null)
            {
                return null;
            }

            var comment = new Comment()
            {
                Content = content,
                PostId = postId,
                User = user
            };

            try
            {
                this.commentsRepo.Add(comment);
                this.commentsRepo.Save();
                return this.commentsRepo.GetById(comment.Id);
            }
            catch (Exception)
            {
                // log error
                return null;
            }
        }
    }
}
