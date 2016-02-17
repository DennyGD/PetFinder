namespace PetFinder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    using PetFinder.Common.Constants;

    public class Region : BaseModel<int>
    {
        [Required]
        [MaxLength(Models.RegionNameMaxLength)]
        [MinLength(Models.RegionNameMinLength)]
        public string Name { get; set; }
    }
}
