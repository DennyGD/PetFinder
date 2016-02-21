namespace PetFinder.Services.Data
{
    using System.Linq;

    using Contracts;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;

    public class RegionsService : IRegionsService
    {
        private readonly IDbRepository<Region> regionsRepo;

        public RegionsService(IDbRepository<Region> regionsRepo)
        {
            this.regionsRepo = regionsRepo;
        }

        public IQueryable<Region> All(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.regionsRepo.AllWithDeleted();
            }
            else
            {
                return this.regionsRepo.All();
            }
        }
    }
}
