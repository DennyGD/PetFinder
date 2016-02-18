namespace PetFinder.Web.Controllers
{
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using System.Linq;
    using System.Web.Mvc;
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

        // TODO refactor
        [ChildActionOnly]
        //[OutputCache(Duration = 2 * 60)]
        public ActionResult LastLostPosts()
        {
            var data = this.postsService
                .GetLastByCategory("Изгубени")
                .To<PostBaseViewModel>()
                .ToList();

            return this.PartialView("_PostBaseListPartial", data);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 2 * 60)]
        public ActionResult LastFoundPets()
        {
            var data = this.postsService
                .GetLastByCategory("Намерени")
                .To<PostBaseViewModel>()
                .ToList();

            return this.PartialView("_PostBaseListPartial", data);
        }
    }
}
