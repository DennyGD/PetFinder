namespace PetFinder.Web.Areas.Private.Controllers
{
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using ViewModels;
    using System.Linq;
    using Web.ViewModels.Posts;
    using Infrastructure.Mapping;
    public class UsersController : BasePrivateController
    {
        public UsersController(IUsersService usersService)
            : base(usersService)
        {
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            var data = this.Mapper.Map<UserViewModel>(this.CurrentUser);
            return this.View(data);
        }

        [HttpGet]
        public ActionResult MyPosts()
        {
            var data = this.CurrentUser
                .Posts
                .AsQueryable()
                .To<PostBaseViewModel>()
                .ToList();

            return this.View(data);
        }
    }
}