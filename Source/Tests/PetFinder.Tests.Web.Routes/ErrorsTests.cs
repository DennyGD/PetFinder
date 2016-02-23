namespace PetFinder.Tests.Web.Routes
{
    using System.Web.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MvcRouteTester;
    using PetFinder.Web;
    using PetFinder.Web.Controllers;

    [TestClass]
    public class ErrorsTests
    {
        private const string ControllerName = "Errors";

        private RouteCollection routes;

        [TestInitialize]
        public void Initialize()
        {
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [TestMethod]
        public void ErrorsIndexShouldBeMapped()
        {
            this.routes
                .ShouldMap($"/{ControllerName}/Index")
                .To<ErrorsController>(x => x.Index());
        }

        [TestMethod]
        public void ErrorsNotFoundShouldBeMapped()
        {
            this.routes
                .ShouldMap($"/{ControllerName}/NotFound")
                .To<ErrorsController>(x => x.NotFound());
        }
    }
}
