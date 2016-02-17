namespace PetFinder.Web.Controllers
{
    using Services.Data.Contracts;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        private readonly IPostsService postsService;

        public HomeController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public ActionResult Index()
        {
            var postsCount = this.postsService.GetLastByCategory("Изгубени").ToList().Count;
            ViewBag.Count = postsCount;
            return this.View();
        }
    }
}
