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
    public class PetsController : BaseAdminController
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pets_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Pet> pets = db.Pets;
            DataSourceResult result = pets.ToDataSourceResult(request, pet => new {
                Id = pet.Id,
                Name = pet.Name,
                CreatedOn = pet.CreatedOn,
                ModifiedOn = pet.ModifiedOn,
                IsDeleted = pet.IsDeleted,
                DeletedOn = pet.DeletedOn
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Pets_Create([DataSourceRequest]DataSourceRequest request, Pet pet)
        {
            if (ModelState.IsValid)
            {
                var entity = new Pet
                {
                    Name = pet.Name,
                    CreatedOn = pet.CreatedOn,
                    ModifiedOn = pet.ModifiedOn,
                    IsDeleted = pet.IsDeleted,
                    DeletedOn = pet.DeletedOn
                };

                db.Pets.Add(entity);
                db.SaveChanges();
                pet.Id = entity.Id;
            }

            return Json(new[] { pet }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Pets_Update([DataSourceRequest]DataSourceRequest request, Pet pet)
        {
            if (ModelState.IsValid)
            {
                var entity = new Pet
                {
                    Id = pet.Id,
                    Name = pet.Name,
                    CreatedOn = pet.CreatedOn,
                    ModifiedOn = pet.ModifiedOn,
                    IsDeleted = pet.IsDeleted,
                    DeletedOn = pet.DeletedOn
                };

                db.Pets.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { pet }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Pets_Destroy([DataSourceRequest]DataSourceRequest request, Pet pet)
        {
            if (ModelState.IsValid)
            {
                var entity = new Pet
                {
                    Id = pet.Id,
                    Name = pet.Name,
                    CreatedOn = pet.CreatedOn,
                    ModifiedOn = pet.ModifiedOn,
                    IsDeleted = pet.IsDeleted,
                    DeletedOn = pet.DeletedOn
                };

                db.Pets.Attach(entity);
                db.Pets.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { pet }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
