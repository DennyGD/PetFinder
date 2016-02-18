namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface IUsersService
    {
        IQueryable<User> All(bool includeDeleted);

        User ById(string id);

        void Update(string email, string firstName, string lastName, string id);
    }
}
