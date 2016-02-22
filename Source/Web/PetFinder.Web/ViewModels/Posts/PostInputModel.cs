namespace PetFinder.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using Common.Constants;

    public class PostInputModel
    {
        [Required]
        [MaxLength(Models.PostTitleMaxLength)]
        [MinLength(Models.PostTitleMinLength)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required]
        [MaxLength(Models.PostContentMaxLength)]
        [MinLength(Models.PostContentMinLength)]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        //[Required]
        //public DateTime EventTime { get; set; }

        public int RegionId { get; set; }

        public int PostCategoryId { get; set; }

        public int PetId { get; set; }

        public IEnumerable<HttpPostedFileBase> UploadedFiles { get; set; }
    }
}