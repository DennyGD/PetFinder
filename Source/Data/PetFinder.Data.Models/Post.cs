namespace PetFinder.Data.Models
{
    using Common.Models;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetFinder.Common.Constants;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Post : BaseModel<int>
    {
        private ICollection<Comment> comments;

        public Post()
        {
            this.comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(Models.PostTitleMaxLength)]
        [MinLength(Models.PostTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(Models.PostContentMaxLength)]
        [MinLength(Models.PostContentMinLength)]
        public string Content { get; set; }

        [Required]
        public DateTime EventTime { get; set; }

        public bool IsSolved { get; set; }

        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public int PostCategoryId { get; set; }

        public virtual PostCategory PostCategory { get; set; }

        public int PetId { get; set; }

        public virtual Pet Pet { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
