namespace PetFinder.Tests.Web.Controllers
{
    using System.Collections.Generic;

    using Common.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using PetFinder.Web.Controllers;
    using PetFinder.Web.Infrastructure.Mapping;
    using PetFinder.Web.ViewModels.Posts;

    using TestStack.FluentMVCTesting;

    [TestClass]
    public class HomeControllerTests
    {
        private static HomeController controller;

        private static AutoMapperConfig autoMapperConfig;

        [ClassInitialize]
        public static void Initialize(TestContext test)
        {
            autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(HomeController).Assembly);

            var postsService = MocksFactory.GetPostsService();
            controller = new HomeController(postsService);
        }

        [TestMethod]
        public void WhenIndexIsCalledDefaultViewShouldBeRendered()
        {
            controller
                .WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void LostPetsIsCalledAsChildActionAndPartialViewWithDataShouldBeReturned()
        {
            controller
                .WithCallToChild(x => x.LastLostPosts())
                .ShouldRenderPartialView(Others.PostBaseListPartial)
                .WithModel<IEnumerable<PostBaseViewModel>>();
        }

        [TestMethod]
        public void FoundPetsIsCalledAsChildActionAndPartialViewWithDataShouldBeReturned()
        {
            controller
                .WithCallToChild(x => x.LastLostPosts())
                .ShouldRenderPartialView(Others.PostBaseListPartial)
                .WithModel<IEnumerable<PostBaseViewModel>>();
        }
    }
}
