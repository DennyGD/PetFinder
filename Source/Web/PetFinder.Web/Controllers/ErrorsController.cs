namespace PetFinder.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            this.Response.StatusCode = 400;
            return this.View();
        }

        [HttpGet]
        public ActionResult NotFound()
        {
            this.Response.StatusCode = 404;
            return this.View();
        }
    }
}