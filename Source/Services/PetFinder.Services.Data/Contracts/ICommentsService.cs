namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface ICommentsService
    {
        IQueryable<Comment> AllByPostId(int postId, int? takeSize);

        Comment Add(string content, int postId, string userId);
    }
}
