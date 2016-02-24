namespace PetFinder.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using ViewModels.Statistics;

    public class StatisticsController : BaseController
    {
        private readonly IPostsService postsService;

        public StatisticsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult BasicStats()
        {
            var lostPetsCount = this.Cache
                .Get(
                "lostPetsCount",
                () => this.postsService.All(false, "изгубени").ToList().Count,
                30 * 60);

            var foundPetsCount = this.Cache
                .Get(
                "foundPetsCount",
                () => this.postsService.All(false, "намерени").ToList().Count,
                30 * 60);

            var solvedCases = this.Cache
                .Get(
                "solvedCases",
                () => this.postsService.All(true).ToList().Count,
                30 * 60);

            var data = new StatsViewModel { LostPets = lostPetsCount, FoundPets = foundPetsCount, SolvedCases = solvedCases };

            return this.PartialView("_StatsPartial", data);
        }
    }
}