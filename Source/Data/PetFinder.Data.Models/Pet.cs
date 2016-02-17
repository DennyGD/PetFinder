namespace PetFinder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    using PetFinder.Common.Constants;

    public class Pet : BaseModel<int>
    {
        [Required]
        [MaxLength(Models.PetNameMaxLength)]
        public string Name { get; set; }
    }
}
