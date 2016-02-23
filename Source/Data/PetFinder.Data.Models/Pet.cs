namespace PetFinder.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using PetFinder.Common.Constants;
    
    public class Pet : BaseModel<int>
    {
        private ICollection<Post> posts;

        public Pet()
        {
            this.posts = new HashSet<Post>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(Models.PetNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }
    }
}
