namespace PetFinder.Tests.Web.Controllers
{
    using System.Web.Mvc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mocks;
    using PetFinder.Web.Controllers;

    using TestStack.FluentMVCTesting;

    [TestClass]
    public class ErrorsControllerTests
    {
        private ErrorsController controller;

        [TestInitialize]
        public void Initialize()
        {
            var httpContext = MocksFactory.GetHttpContext();
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            this.controller = new ErrorsController()
            {
                ControllerContext = controllerContext
            };
        }

        [TestMethod]
        public void WhenIndexIsCalledDefaultViewShouldBeReturned()
        {
            this.controller
                .WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void WhenNotFoundIsCalledDefaultViewShouldBeReturned()
        {
            this.controller
                .WithCallTo(x => x.NotFound())
                .ShouldRenderDefaultView();
        }
    }
}
