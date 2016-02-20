namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface IUsersService
    {
        IQueryable<User> All(bool includeDeleted);

        User ById(string id, bool includeDeleted);

        bool Update(string email, string firstName, string lastName, bool isDeleted, string id);
    }
}
