namespace PetFinder.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Common.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    using PetFinder.Data.Models;

    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // TODO add db tables
        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Pet> Pets { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<PostCategory> PostCategories { get; set; }

        public virtual IDbSet<Region> Regions { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //When a category is deleted all its articles are deleted as well.
            //modelBuilder
            //    .Entity<Article>()
            //    .HasRequired(p => p.Category)
            //    .WithMany(x => x.Articles)
            //    .WillCascadeOnDelete(true);

            //base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Post>()
                .HasRequired(x => x.User)
                .WithMany(x => x.Posts)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
