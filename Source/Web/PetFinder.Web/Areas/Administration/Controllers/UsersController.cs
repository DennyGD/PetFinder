namespace PetFinder.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using ViewModels;

    public class UsersController : BaseAdminController
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
    }
}
