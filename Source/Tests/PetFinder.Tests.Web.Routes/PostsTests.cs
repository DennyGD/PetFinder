namespace PetFinder.Tests.Web.Routes
{
    using System.Web.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MvcRouteTester;
    using PetFinder.Web;
    using PetFinder.Web.Controllers;

    [TestClass]
    public class PostsTests
    {
        private const string ControllerName = "Posts";

        private RouteCollection routes;

        [TestInitialize]
        public void Initialize()
        {
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [TestMethod]
        public void PostsAllShouldBeMapped()
        {
            this.routes
                .ShouldMap($"/{ControllerName}/All/1")
                .To<PostsController>(x => x.All(1));
        }

        [TestMethod]
        public void PostsDetailsWithSomeIdShouldBeMapped()
        {
            int id = 1;
            this.routes
                .ShouldMap($"/{ControllerName}/Details/{id}")
                .To<PostsController>(x => x.Details(id));
        }

        [TestMethod]
        public void PostsAddShouldBeMapped()
        {
            this.routes
                .ShouldMap($"/{ControllerName}/Add")
                .To<PostsController>(x => x.Add());
        }
    }
}
