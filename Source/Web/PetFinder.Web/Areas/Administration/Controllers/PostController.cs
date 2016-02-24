namespace PetFinder.Web.Areas.Administration.Controllers
{
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using ViewModels;

    public class PostController : BaseAdminController
    {
        private readonly IPostsService postsService;

        public PostController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Posts_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.postsService
                .All(true)
                .To<PostAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Posts_Update([DataSourceRequest]DataSourceRequest request, PostAdminViewModel post)
        {
            if (this.ModelState.IsValid)
            {
                this.postsService.Update(post.Title, post.Content, post.IsDeleted, post.Id);
            }

            var postById = this.postsService.All(true).Where(x => x.Id == post.Id).FirstOrDefault();
            var data = this.Mapper.Map<PostAdminViewModel>(postById);
            return this.Json(new[] { data }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Posts_Destroy([DataSourceRequest]DataSourceRequest request, PostAdminViewModel post)
        {
            if (post != null)
            {
                this.postsService.HardDelete(post.Id);
            }

            return this.Json(new[] { post }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
