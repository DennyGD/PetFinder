namespace PetFinder.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CommentAdminViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(Models.CommentContentMaxLength)]
        [MinLength(1)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}