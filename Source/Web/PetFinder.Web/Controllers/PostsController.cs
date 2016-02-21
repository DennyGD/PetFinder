namespace PetFinder.Web.Controllers
{
    using System;
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
        public ActionResult All(int id = 1)
        {
            var posts = this.postsService
                .All(id, 2)
                .To<PostBaseViewModel>()
                .ToList();

            var totalPages = (int)Math.Ceiling(this.postsService.AllPostsCount() / (decimal)2);
            var data = new AllPageViewModel()
            {
                CurrentPage = id,
                TotalPages = totalPages,
                Posts = posts
            };

            return this.View(data);
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