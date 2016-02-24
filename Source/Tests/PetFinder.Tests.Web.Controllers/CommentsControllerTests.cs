namespace PetFinder.Tests.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Common.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using PetFinder.Web.Controllers;
    using PetFinder.Web.Infrastructure.Mapping;
    using PetFinder.Web.ViewModels.Comments;

    using TestStack.FluentMVCTesting;

    [TestClass]
    public class CommentsControllerTests
    {
        private static CommentsController controller;

        private static AutoMapperConfig autoMapperConfig;

        [ClassInitialize]
        public static void Initialize(TestContext test)
        {
            autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(CommentsController).Assembly);

            var commentsService = MocksFactory.GetCommentsService();
            controller = new CommentsController(commentsService);
        }

        [TestMethod]
        public void WhenAllIsCalledPartialViewWithDataShouldBeReturned()
        {
            controller
                .WithCallTo(x => x.AllCommentsForPost(1))
                .ShouldRenderPartialView(Others.CommentsListPartial)
                .WithModel<IEnumerable<CommentViewModel>>();
        }

        [TestMethod]
        public void WhenAddIsCalledAndUserIsUnauthorizedContentShouldBeReturned()
        {
            var httpContext = MocksFactory.GetHttpContext();
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            controller.ControllerContext = controllerContext;

            controller
                .WithCallTo(x => x.Add(new CommentInputModel()))
                .ShouldReturnContent();
        }
    }
}
