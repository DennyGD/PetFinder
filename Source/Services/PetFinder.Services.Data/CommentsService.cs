namespace PetFinder.Services.Data
{
    using System.Linq;

    using Contracts;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;
    
    public class CommentsService : ICommentsService
    {
        private readonly IDbRepository<Comment> commentsRepo;

        public CommentsService(IDbRepository<Comment> commentsRepo)
        {
            this.commentsRepo = commentsRepo;
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
    }
}
