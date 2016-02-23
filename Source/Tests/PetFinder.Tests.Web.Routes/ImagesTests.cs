namespace PetFinder.Tests.Web.Routes
{
    using System.Web.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MvcRouteTester;
    using PetFinder.Web;
    using PetFinder.Web.Controllers;

    [TestClass]
    public class ImagesTests
    {
        private const string ControllerName = "Images";

        private RouteCollection routes;

        [TestInitialize]
        public void Initialize()
        {
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [TestMethod]
        public void ImagesAllWithSomeIdShouldBeMapped()
        {
            int id = 5;
            this.routes
                .ShouldMap($"/{ControllerName}/All/{id}")
                .To<ImagesController>(x => x.All(id));
        }
    }
}
