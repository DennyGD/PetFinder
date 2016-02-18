namespace PetFinder.Services.Data
{
    using System.Linq;

    using Contracts;
    using PetFinder.Data.Models;
    using PetFinder.Data.Common;

    public class UsersService : IUsersService
    {
        private readonly IDbRepository<User> usersRepo;

        public UsersService(IDbRepository<User> usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public IQueryable<User> All(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.usersRepo.AllWithDeleted();
            }
            else
            {
                return this.usersRepo.All();
            }
        }
    }
}
