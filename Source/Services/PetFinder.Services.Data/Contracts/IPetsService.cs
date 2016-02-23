namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface IPetsService
    {
        IQueryable<Pet> All(bool includeDeleted);

        Pet ById(int id, bool includeDeleted);

        Pet Add(string name);

        void Update(string name, bool isDeleted, int id);
    }
}
