namespace PetFinder.Web.ViewModels.Posts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class PostInputModel
    {
        [Required]
        [MaxLength(Models.PostTitleMaxLength)]
        [MinLength(Models.PostTitleMinLength)]
        public string Title { get; set; }

        //[Required]
        //[MaxLength(Models.PostContentMaxLength)]
        //[MinLength(Models.PostContentMinLength)]
        //public string Content { get; set; }

        //[Required]
        //public DateTime EventTime { get; set; }

        public int RegionId { get; set; }

        public int PostCategoryId { get; set; }

        public int PetId { get; set; }
    }
}