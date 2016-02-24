namespace PetFinder.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;
    
    public class PetAdminViewModel : BaseAdminViewModel, IMapFrom<Pet>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(Models.PetNameMaxLength)]
        public string Name { get; set; }
    }
}