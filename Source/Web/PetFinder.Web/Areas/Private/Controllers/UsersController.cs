namespace PetFinder.Web.Areas.Private.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels;
    using Web.ViewModels.Posts;

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

        [HttpGet]
        public ActionResult MyComments()
        {
            var data = this.CurrentUser
                .Comments
                .AsQueryable()
                .OrderByDescending(x => x.CreatedOn)
                .To<CommentPrivateViewModel>()
                .ToList();

            return this.View(data);
        }
    }
}