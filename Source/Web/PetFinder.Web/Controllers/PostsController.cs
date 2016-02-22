﻿namespace PetFinder.Web.Controllers
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
    using ViewModels.Shared;
    using Services.Web;

    public class PostsController : BaseController
    {
        private const int DefaultPageSize = 5;

        private readonly IPostsService postsService;

        private readonly ICommentsService commentsService;

        private readonly IRegionsService regionsService;

        private readonly IPostCategoriesService postCategoriesService;

        private readonly IPetsService petsService;

        public IDropdownListService Dropdown { get; set; }

        public PostsController(
            IPostsService postsService, 
            ICommentsService commentsService, 
            IRegionsService regionsService, 
            IPostCategoriesService postCategoriesService, 
            IPetsService petsService)
            : base()
        {
            this.postsService = postsService;
            this.commentsService = commentsService;
            this.regionsService = regionsService;
            this.postCategoriesService = postCategoriesService;
            this.petsService = petsService;
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
            var pageInfo = new PageInfo() { CurrentPage = id, TotalPages = totalPages };
            var data = new AllPageViewModel()
            {
                PageInfo = pageInfo,
                Posts = posts
            };

            ViewBag.Selected = queryForRegion;
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

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PostInputModel post)
        {
            return null;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Regions(bool toUseForAddition, string name, string selected = null)
        {
            var allRegions = this.Cache
                .Get("allRegions",
                () => this.regionsService.All(false).ToList(),
                30 * 60);

            IEnumerable<SelectListItem> data = null;
            if (toUseForAddition)
            {
                data = this.Dropdown.RegionsForAddition(allRegions);
            }
            else
            {
                data = this.Dropdown.RegionsForSearch(allRegions, selected);
            }

            this.ViewBag.Name = name;
            return this.PartialView(Others.DropdownPartialName, data);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult PostCategories(string name)
        {
            var allPostCategories = this.Cache
                .Get("allPostCategories",
                () => this.postCategoriesService.All(false).ToList(),
                30 * 60);

            this.ViewBag.Name = name;

            var data = this.Dropdown.PostCategories(allPostCategories);
            return this.PartialView(Others.DropdownPartialName, data);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Pets(string name)
        {
            var allPets = this.Cache
                .Get("allPets",
                () => this.petsService.All(false).ToList(),
                30 * 60);

            this.ViewBag.Name = name;

            var data = this.Dropdown.Pets(allPets);
            return this.PartialView(Others.DropdownPartialName, data);
        }
    }
}