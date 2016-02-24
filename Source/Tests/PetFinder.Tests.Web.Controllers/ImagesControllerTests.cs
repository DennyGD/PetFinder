namespace PetFinder.Tests.Web.Controllers
{
    using System.Collections.Generic;

    using Common.Constants;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mocks;
    using PetFinder.Web.Controllers;
    using PetFinder.Web.Infrastructure.Mapping;
    using PetFinder.Web.ViewModels.Images;

    using TestStack.FluentMVCTesting;

    [TestClass]
    public class ImagesControllerTests
    {
        private static ImagesController controller;

        private static AutoMapperConfig autoMapperConfig;

        [ClassInitialize]
        public static void Initialize(TestContext test)
        {
            autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(ImagesController).Assembly);

            var postsService = MocksFactory.GetPostsService();
            controller = new ImagesController(postsService);
        }

        [TestMethod]
        public void WhenAllIsCalledWithExistentIdDefaultViewWithDataShouldBeReturned()
        {
            controller
                .WithCallTo(x => x.All(1))
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<ImageViewModel>>();
        }

        [TestMethod]
        public void WhenAllIsCalledWithNonExistentIdRedirectShouldHappen()
        {
            controller
                .WithCallTo(x => x.All(0))
                .ShouldRedirectToRoute(string.Empty);
        }

        [TestMethod]
        public void WhenAllIsCalledWithNonExistentIdTempDataShouldBeUsed()
        {
            controller.All(0);
            controller.ShouldHaveTempDataProperty(Others.TempDataForError);
        }
    }
}
