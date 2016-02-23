namespace PetFinder.Web.Areas.Private.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;
    
    public class UserViewModel : IMapFrom<User>
    {
        [Display(Name = "Собствено име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}