namespace PetFinder.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CommentAdminViewModel : BaseAdminViewModel, IMapFrom<Comment>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(Models.CommentContentMaxLength)]
        [MinLength(1)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}