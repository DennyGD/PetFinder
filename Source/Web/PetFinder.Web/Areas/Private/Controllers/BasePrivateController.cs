namespace PetFinder.Web.Areas.Private.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using Web.Controllers;

    [Authorize]
    public class BasePrivateController : BaseController
    {
        public BasePrivateController(IUsersService usersService)
            : base()
        {
            this.UsersService = usersService;
        }

        protected IUsersService UsersService { get; private set; }

        protected User CurrentUser { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            var currentUserId = requestContext.HttpContext.User.Identity.GetUserId();
            this.SetCurrentUser(currentUserId);

            return base.BeginExecute(requestContext, callback, state);
        }

        private void SetCurrentUser(string id)
        {
            this.CurrentUser = this.UsersService.ById(id, false);
        }
    }
}