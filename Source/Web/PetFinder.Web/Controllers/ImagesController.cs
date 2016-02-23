namespace PetFinder.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Common.Constants;
    using Services.Data.Contracts;
    using ViewModels.Images;

    public class ImagesController : BaseController
    {
        private readonly IPostsService postsService;

        public ImagesController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        [HttpGet]
        public ActionResult All(int id)
        {
            var post = this.postsService
                .ById(id);

            if (post == null)
            {
                this.TempData[Others.TempDataForError] = "Не е открит резултат.";
                return this.RedirectToAction("Index", "Home");
            }

            var data = this.Mapper.Map<IEnumerable<ImageViewModel>>(post.Images);
            return this.View(data);
        }
    }
}