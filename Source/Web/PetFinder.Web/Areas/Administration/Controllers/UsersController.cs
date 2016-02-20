namespace PetFinder.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using ViewModels;
    using Web.Controllers;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.usersService
                .All(true)
                .To<UserAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Users_Update([DataSourceRequest]DataSourceRequest request, UserAdminViewModel user)
        {
            var updated = false;
            if (this.ModelState.IsValid)
            {
                updated = this.usersService.Update(user.Email, user.FirstName, user.LastName, user.IsDeleted, user.Id);
            }

            var userById = this.usersService.ById(user.Id, true);
            var data = this.Mapper.Map<UserAdminViewModel>(userById);
            return this.Json(new[] { data }.ToDataSourceResult(request, this.ModelState));
        }

        // [AcceptVerbs(HttpVerbs.Post)]
        // public ActionResult Users_Destroy([DataSourceRequest]DataSourceRequest request, UserAdminViewModel user)
        // {
        //     if (!string.IsNullOrWhiteSpace(user?.Id))
        //     {
        //         this.usersService.Delete(user.Id, false);
        //     }
           
        //     return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        // }
    }
}
