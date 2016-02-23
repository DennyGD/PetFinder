namespace PetFinder.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Posts;

    public class HomeController : BaseController
    {
        private readonly IPostsService postsService;

        public HomeController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 2 * 60)]
        public ActionResult LastLostPosts()
        {
            var data = this.postsService
                .LastByCategory("Изгубени")
                .To<PostBaseViewModel>()
                .ToList();

            return this.PartialView("_PostBaseListPartial", data);
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 2 * 60)]
        public ActionResult LastFoundPets()
        {
            var data = this.postsService
                .LastByCategory("Намерени")
                .To<PostBaseViewModel>()
                .ToList();

            return this.PartialView("_PostBaseListPartial", data);
        }
    }
}
