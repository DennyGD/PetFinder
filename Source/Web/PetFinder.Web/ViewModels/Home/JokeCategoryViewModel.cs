namespace PetFinder.Web.ViewModels.Home
{
    using PetFinder.Data.Models;
    using PetFinder.Web.Infrastructure.Mapping;

    public class JokeCategoryViewModel : IMapFrom<JokeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
