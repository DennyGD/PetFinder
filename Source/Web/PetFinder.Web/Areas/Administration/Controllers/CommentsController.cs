namespace PetFinder.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using PetFinder.Services.Data.Contracts;
    using ViewModels;

    public class CommentsController : BaseAdminController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Comments_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.commentsService
                .All(true)
                .To<CommentAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comments_Update([DataSourceRequest]DataSourceRequest request, CommentAdminViewModel comment)
        {
            if (ModelState.IsValid)
            {
                this.commentsService.Update(comment.Content, comment.IsDeleted, comment.Id);
            }

            var commentById = this.commentsService.ById(comment.Id, true);
            var data = this.Mapper.Map<CommentAdminViewModel>(commentById);
            return this.Json(new[] { data }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comments_Destroy([DataSourceRequest]DataSourceRequest request, CommentAdminViewModel comment)
        {
            this.commentsService.Delete(comment.Id);

            return this.Json(new[] { comment }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
