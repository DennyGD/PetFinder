namespace PetFinder.Tests.Web.Routes
{
    using System.Web.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MvcRouteTester;
    using PetFinder.Web;
    using PetFinder.Web.Controllers;

    [TestClass]
    public class HomeTests
    {
        private const string ControllerName = "Home";

        private RouteCollection routes;

        [TestInitialize]
        public void Initialize()
        {
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [TestMethod]
        public void HomeIndexShouldBeMapped()
        {
            this.routes
                .ShouldMap($"/{ControllerName}/Index")
                .To<HomeController>(x => x.Index());
        }

        [TestMethod]
        public void HomeLostPostsShouldBeMapped()
        {
            this.routes
                .ShouldMap($"/{ControllerName}/LastLostPosts")
                .To<HomeController>(x => x.LastLostPosts());
        }

        [TestMethod]
        public void HomeFoundPostsShouldBeMapped()
        {
            this.routes
                .ShouldMap($"/{ControllerName}/LastFoundPets")
                .To<HomeController>(x => x.LastFoundPets());
        }

        [TestMethod]
        public void SlashShouldBeMapped()
        {
            this.routes
                .ShouldMap("/")
                .From<HomeController>(x => x.Index());
        }
    }
}
