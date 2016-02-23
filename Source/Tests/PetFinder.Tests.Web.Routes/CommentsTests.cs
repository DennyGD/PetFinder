namespace PetFinder.Tests.Web.Routes
{
    using System.Net.Http;
    using System.Web.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MvcRouteTester;
    using PetFinder.Web;
    using PetFinder.Web.Controllers;
    using PetFinder.Web.ViewModels.Comments;

    [TestClass]
    public class CommentsTests
    {
        private const string ControllerName = "Comments";

        private RouteCollection routes;

        [TestInitialize]
        public void Initialize()
        {
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [TestMethod]
        public void CommentsAllWithSomeIdShouldBeMapped()
        {
            int id = 1;
            this.routes
                .ShouldMap($"/{ControllerName}/AllCommentsForPost/{id}")
                .To<CommentsController>(x => x.AllCommentsForPost(1));
        }

        [TestMethod]
        public void CommentsAddWithExpectedModelShouldBeMapped()
        {
            this.routes
                .ShouldMap($"/{ControllerName}/Add")
                .To<CommentsController>(x => x.Add(new CommentInputModel()));
        }
    }
}
