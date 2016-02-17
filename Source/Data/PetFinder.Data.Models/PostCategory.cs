namespace PetFinder.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PetFinder.Data.Common.Models;
    
    public class PostCategory : BaseModel<int>
    {
        [Required]
       // [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
