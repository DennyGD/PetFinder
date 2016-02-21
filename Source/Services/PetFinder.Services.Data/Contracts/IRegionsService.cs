namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface IRegionsService
    {
        IQueryable<Region> All(bool includeDeleted);
    }
}
