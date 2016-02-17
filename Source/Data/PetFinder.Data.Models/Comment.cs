namespace PetFinder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    using PetFinder.Common.Constants;

    public class Comment : BaseModel<int>
    {
        [Required]
        [MaxLength(Models.CommentContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
