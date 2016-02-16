namespace PetFinder.Services.Data
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface IJokesService
    {
        IQueryable<Joke> GetRandomJokes(int count);

        Joke GetById(string id);
    }
}
