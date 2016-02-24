namespace PetFinder.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;
    
    public class PostAdminViewModel : BaseAdminViewModel, IMapFrom<Post>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(Models.PostTitleMaxLength)]
        [MinLength(Models.PostTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(Models.PostContentMaxLength)]
        [MinLength(Models.PostContentMinLength)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public DateTime EventTime { get; set; }

        public bool IsSolved { get; set; }
    }
}