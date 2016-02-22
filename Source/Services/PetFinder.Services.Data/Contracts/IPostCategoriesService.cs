namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface IPostCategoriesService
    {
        IQueryable<PostCategory> All(bool includeDeleted);
    }
}
