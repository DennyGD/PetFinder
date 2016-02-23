namespace PetFinder.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Common.Constants;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using ViewModels;

    public class PetsController : BaseAdminController
    {
        private readonly IPetsService petsService;

        public PetsController(IPetsService petsService)
        {
            this.petsService = petsService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Pets_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.petsService
                .All(true)
                .To<PetAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Pets_Create([DataSourceRequest]DataSourceRequest request, PetAdminViewModel pet)
        {
            if (pet == null || string.IsNullOrWhiteSpace(pet.Name) || pet.Name.Length > Models.PetNameMaxLength)
            {
                return this.Json(new[] { pet }.ToDataSourceResult(request, this.ModelState));
            }

            var newPet = this.petsService.Add(pet.Name);
            if (newPet == null)
            {
                return this.Json(new[] { pet }.ToDataSourceResult(request, this.ModelState));
            }

            var data = this.Mapper.Map<PetAdminViewModel>(newPet);
            return this.Json(new[] { data }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Pets_Update([DataSourceRequest]DataSourceRequest request, PetAdminViewModel pet)
        {
            if (this.ModelState.IsValid)
            {
                this.petsService.Update(pet.Name, pet.IsDeleted, pet.Id);
            }

            var petById = this.petsService.ById(pet.Id, true);
            var data = this.Mapper.Map<PetAdminViewModel>(petById);
            return this.Json(new[] { data }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
