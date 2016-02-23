namespace PetFinder.Web.Areas.Administration.ViewModels
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class PetAdminViewModel : BaseAdminViewModel, IMapFrom<Pet>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}