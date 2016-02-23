namespace PetFinder.Tests.Web.Routes
{
    using System.Web.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MvcRouteTester;
    using PetFinder.Web;
    using PetFinder.Web.Controllers;

    [TestClass]
    public class StatisticsTests
    {
        private const string ControllerName = "Statistics";

        private RouteCollection routes;

        [TestInitialize]
        public void Initialize()
        {
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [TestMethod]
        public void StatisticsBasicShouldBeMapped()
        {
            this.routes
                .ShouldMap($"/{ControllerName}/BasicStats")
                .To<StatisticsController>(x => x.BasicStats());
        }
    }
}
