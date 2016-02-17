namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface IPostsService
    {
        IQueryable<Post> GetLastByCategory(string category, int count = 5);
    }
}
