namespace PetFinder.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using PetFinder.Web.Controllers;

    [Authorize(Roles = "Admin")]
    public class AdministrationController : BaseController
    {
    }
}
