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

namespace PetFinder.Web.Areas.Administration.Controllers
{
    public class CommentsController : BaseAdminController
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Comments_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Comment> comments = db.Comments;
            DataSourceResult result = comments.ToDataSourceResult(request, comment => new {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                ModifiedOn = comment.ModifiedOn,
                IsDeleted = comment.IsDeleted,
                DeletedOn = comment.DeletedOn
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comments_Update([DataSourceRequest]DataSourceRequest request, Comment comment)
        {
            if (ModelState.IsValid)
            {
                var entity = new Comment
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedOn = comment.CreatedOn,
                    ModifiedOn = comment.ModifiedOn,
                    IsDeleted = comment.IsDeleted,
                    DeletedOn = comment.DeletedOn
                };

                db.Comments.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { comment }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comments_Destroy([DataSourceRequest]DataSourceRequest request, Comment comment)
        {
            if (ModelState.IsValid)
            {
                var entity = new Comment
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedOn = comment.CreatedOn,
                    ModifiedOn = comment.ModifiedOn,
                    IsDeleted = comment.IsDeleted,
                    DeletedOn = comment.DeletedOn
                };

                db.Comments.Attach(entity);
                db.Comments.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { comment }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
