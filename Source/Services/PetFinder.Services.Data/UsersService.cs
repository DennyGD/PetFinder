namespace PetFinder.Services.Data
{
    using System;
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

        public User ById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            return this.usersRepo.GetById(id);
        }

        public void Update(string email, string firstName, string lastName, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            var userToUpdate = this.usersRepo.GetById(id);
            if (userToUpdate == null)
            {
                return;
            }

            userToUpdate.Email = email;
            userToUpdate.FirstName = firstName;
            userToUpdate.LastName = lastName;

            try
            {
                this.usersRepo.Save();
            }
            catch (Exception)
            {
                // TODO log exception in some error logger
            }
        }
    }
}
