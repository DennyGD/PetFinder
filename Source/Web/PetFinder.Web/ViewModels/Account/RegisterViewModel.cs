namespace PetFinder.Web.ViewModels.Account
{
    using Common.Constants;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Собствено име")]
        [MaxLength(Models.FirstNameMaxLength, ErrorMessage = "Максимална дължина на {0} - {1} символа.")]
        [MinLength(Models.FirstNameMinLength, ErrorMessage = "Минимална дължина на {0} - {1} символа.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [MaxLength(Models.LastNameMaxLength, ErrorMessage = "Максимална дължина на {0} - {1} символа.")]
        [MinLength(Models.LastNameMinLength, ErrorMessage = "Минимална дължина на {0} - {1} символа.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
