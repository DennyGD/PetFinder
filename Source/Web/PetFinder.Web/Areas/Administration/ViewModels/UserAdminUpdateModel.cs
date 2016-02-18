namespace PetFinder.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class UserAdminUpdateModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(Models.FirstNameMaxLength)]
        [MinLength(Models.FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(Models.LastNameMaxLength)]
        [MinLength(Models.LastNameMinLength)]
        public string LastName { get; set; }
    }
}