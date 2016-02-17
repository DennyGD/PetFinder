namespace PetFinder.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using PetFinder.Common.Constants;
    using Data.Common.Models;
    
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser, IDeletableEntity, IAuditInfo
    {
        private ICollection<Post> posts;

        private ICollection<Comment> comments;

        public User()
            : base()
        {
            this.posts = new HashSet<Post>();
            this.comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(Models.FirstNameMaxLength)]
        [MinLength(Models.FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(Models.LastNameMaxLength)]
        [MinLength(Models.LastNameMinLength)]
        public string LastName { get; set; }

        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
