namespace PetFinder.Services.Data
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<JokeCategory> GetAll();

        JokeCategory EnsureCategory(string name);
    }
}
