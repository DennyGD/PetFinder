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
    }
}