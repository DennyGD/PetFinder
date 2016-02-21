namespace PetFinder.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using ViewModels.Posts;
    using ViewModels.Comments;
    using Infrastructure.Mapping;
    using PetFinder.Common.Constants;

    public class PostsController : BaseController
    {
        private const int DefaultPageSize = 2;

        private readonly IPostsService postsService;

        private readonly ICommentsService commentsService;

        private readonly IRegionsService regionsService;

        public PostsController(IPostsService postsService, ICommentsService commentsService, IRegionsService regionsService)
            : base()
        {
            this.postsService = postsService;
            this.commentsService = commentsService;
            this.regionsService = regionsService;
        }

        [HttpGet]
        public ActionResult All(int id = 1)
        {
            var queryForRegion = this.HttpContext.Request.QueryString["Region"];
            var posts = this.postsService
                .All(id, DefaultPageSize, queryForRegion)
                .To<PostBaseViewModel>()
                .ToList();

            var totalPages = (int)Math.Ceiling(this.postsService.AllPostsCount(queryForRegion) / (decimal)DefaultPageSize);
            var data = new AllPageViewModel()
            {
                CurrentPage = id,
                TotalPages = totalPages,
                Posts = posts
            };

            ViewBag.Region = this.GetRegions(queryForRegion);
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

        private IEnumerable<SelectListItem> GetRegions(string defaultSelectedRegion)
        {
            // TODO should cache this
            var allRegions = this.regionsService.All(false).ToList();

            defaultSelectedRegion = defaultSelectedRegion ?? Others.AllRegions;

            var result = new List<SelectListItem>();
            result.Add(new SelectListItem() { Text = Others.AllRegions, Value = Others.AllRegions, Selected = defaultSelectedRegion == Others.AllRegions });
            allRegions.ForEach(x => result.Add(new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name,
                Selected = x.Name == defaultSelectedRegion
            }));

            return result;
        }
    }
}