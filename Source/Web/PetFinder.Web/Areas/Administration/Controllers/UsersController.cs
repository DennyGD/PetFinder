namespace PetFinder.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using PetFinder.Data.Models;
    using PetFinder.Data;
    using Services.Data.Contracts;
    using Infrastructure.Mapping;
    using ViewModels;
    using Web.Controllers;
    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.usersService
                .All(true)
                .To<UserAdminViewModel>()
                .ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Update([DataSourceRequest]DataSourceRequest request, UserAdminUpdateModel user)
        {
            if (ModelState.IsValid)
            {
                this.usersService.Update(user.Email, user.FirstName, user.LastName, user.Id);
            }

            var userById = this.usersService.ById(user.Id);
            var data = this.Mapper.Map<UserAdminViewModel>(userById);
            return Json(new[] { data }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Destroy([DataSourceRequest]DataSourceRequest request, User user)
        {
            if (ModelState.IsValid)
            {
                var entity = new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CreatedOn = user.CreatedOn,
                    ModifiedOn = user.ModifiedOn,
                    DeletedOn = user.DeletedOn,
                    IsDeleted = user.IsDeleted,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    LockoutEndDateUtc = user.LockoutEndDateUtc,
                    LockoutEnabled = user.LockoutEnabled,
                    AccessFailedCount = user.AccessFailedCount,
                    UserName = user.UserName
                };

                db.Users.Attach(entity);
                db.Users.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
