namespace PetFinder.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using PetFinder.Common.Constants;
    
    public class Pet : BaseModel<int>
    {
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(Models.PetNameMaxLength)]
        public string Name { get; set; }
    }
}
