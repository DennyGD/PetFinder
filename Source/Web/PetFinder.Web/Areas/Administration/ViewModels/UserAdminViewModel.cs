namespace PetFinder.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;
    using PetFinder.Common.Constants;

    public class UserAdminViewModel : BaseAdminViewModel, IMapFrom<User>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(Models.FirstNameMaxLength)]
        [MinLength(Models.FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(Models.LastNameMaxLength)]
        [MinLength(Models.LastNameMinLength)]
        public string LastName { get; set; }

        public string UserName { get; set; }
    }
}