namespace PetFinder.Services.Data
{
    using System.Linq;

    using Contracts;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;

    public class PetsService : IPetsService
    {
        private readonly IDbRepository<Pet> petsRepo;

        public PetsService(IDbRepository<Pet> petsRepo)
        {
            this.petsRepo = petsRepo;
        }

        public IQueryable<Pet> All(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.petsRepo.AllWithDeleted();
            }
            else
            {
                return this.petsRepo.All();
            }
        }
    }
}
