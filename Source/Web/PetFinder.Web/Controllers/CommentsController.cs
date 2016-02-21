namespace PetFinder.Web.Controllers
{
    using System.Web.Mvc;

    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using PetFinder.Common.Constants;
    using ViewModels.Comments;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        // TODO AJAX only!
        [HttpGet]
        public ActionResult AllCommentsForPost(int id)
        {
            var data = this.commentsService
                .AllByPostId(id, null)
                .To<CommentViewModel>()
                .ToList();

            return this.PartialView("_CommentsListPartial", data);
        }

        // TODO AJAX only!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CommentInputModel comment)
        {
            if (!this.User.Identity.IsAuthenticated)
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