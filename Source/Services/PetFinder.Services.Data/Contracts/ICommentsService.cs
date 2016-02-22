namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface ICommentsService
    {
        IQueryable<Comment> All(bool includeDeleted);

        IQueryable<Comment> AllByPostId(int postId, int? takeSize);

        Comment ById(int id, bool includeDeleted);

        Comment Add(string content, int postId, string userId);

        void Update(string content, bool isDeleted, int id);

        void Delete(int id);
    }
}
