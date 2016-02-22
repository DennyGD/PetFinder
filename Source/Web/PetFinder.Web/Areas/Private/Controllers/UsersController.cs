namespace PetFinder.Web.Areas.Private.Controllers
{
    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class UsersController : BasePrivateController
    {
        public UsersController(IUsersService usersService)
            : base(usersService)
        {
        }

        public ActionResult MyProfile()
        {
            var fullName = this.CurrentUser.FirstName + " " + this.CurrentUser.LastName;
            this.ViewBag.Name = fullName;
            return this.View();
        }
    }
}