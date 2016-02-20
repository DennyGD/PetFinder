namespace PetFinder.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using PetFinder.Common.Constants;
    using Web.Controllers;

    [Authorize(Roles = Models.AdminRole)]
    public abstract class BaseAdminController : BaseController
    {
    }
}