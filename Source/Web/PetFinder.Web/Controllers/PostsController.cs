namespace PetFinder.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using ViewModels.Posts;
    using ViewModels.Comments;
    using Infrastructure.Mapping;
    using PetFinder.Common.Constants;

    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;

        private readonly ICommentsService commentsService;

        public PostsController(IPostsService postsService, ICommentsService commentsService)
            : base()
        {
            this.postsService = postsService;
            this.commentsService = commentsService;
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

            var postMainInfo = this.Mapper.Map<PostExtendedViewModel>(postById);
            var postComments = this.commentsService
                .AllByPostId(id, Others.DefaultTakeSizeForComments)
                .To<CommentViewModel>()
                .ToList();

            var data = new DetailsPageViewModel()
            {
                MainInfo = postMainInfo,
                Comments = postComments
            };
            this.ViewBag.PostId = postMainInfo.Id;

            return this.View(data);
        }
    }
}