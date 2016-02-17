namespace PetFinder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PetFinder.Data.Common.Models;

    public class PostCategory : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
