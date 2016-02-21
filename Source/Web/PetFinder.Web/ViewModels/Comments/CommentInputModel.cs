namespace PetFinder.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using PetFinder.Common.Constants;

    public class CommentInputModel
    {
        [Required]
        [MaxLength(Models.CommentContentMaxLength)]
        public string Content { get; set; }

        public int PostId { get; set; }
    }
}