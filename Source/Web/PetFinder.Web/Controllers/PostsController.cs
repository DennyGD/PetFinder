namespace PetFinder.Web.Controllers
{
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using ViewModels.Posts;

    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
            : base()
        {
            this.postsService = postsService;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var postById = this.postsService
                .ById(id);

            if (postById == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var data = this.Mapper.Map<PostExtendedViewModel>(postById);

            return this.View(data);
        }
    }
}