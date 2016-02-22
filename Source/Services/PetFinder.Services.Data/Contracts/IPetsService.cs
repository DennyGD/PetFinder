namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface IPetsService
    {
        IQueryable<Pet> All(bool includeDeleted);
    }
}
