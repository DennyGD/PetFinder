namespace PetFinder.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Common.Constants;
    using Infrastructure.Filters;
    using Infrastructure.Mapping;

    using Microsoft.AspNet.Identity;

    using Services.Data.Contracts;
    using ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AllCommentsForPost(int id)
        {
            var data = this.commentsService
                .AllByPostId(id, null)
                .To<CommentViewModel>()
                .ToList();

            return this.PartialView(Others.CommentsListPartial, data);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CommentInputModel comment)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                this.Response.StatusCode = 401;
                return this.Content("Unauthorized");
            }

            if (comment == null || !this.ModelState.IsValid)
            {
                this.Response.StatusCode = 400;
                return this.Content("Bad request.");
            }

            var newComment = this.commentsService.Add(comment.Content, comment.PostId, this.User.Identity.GetUserId());
            if (newComment == null)
            {
                this.Response.StatusCode = 400;
                return this.Content("Bad request.");
            }

            return this.PartialView("_SingleCommentPartial", this.Mapper.Map<CommentViewModel>(newComment));
        }
    }
}