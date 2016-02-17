namespace PetFinder.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using PetFinder.Common.Constants;
    
    public class Region : BaseModel<int>
    {
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(Models.RegionNameMaxLength)]
        [MinLength(Models.RegionNameMinLength)]
        public string Name { get; set; }
    }
}
