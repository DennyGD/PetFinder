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

        public Pet ById(int id, bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.petsRepo.AllWithDeleted().Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                return this.petsRepo.GetById(id);
            }
        }
    }
}
