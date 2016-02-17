namespace PetFinder.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using PetFinder.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(AppDbContext context)
        {
            // System.Data.Entity.Core.UpdateException
            if (context.Users.Any())
            {
                return;
            }

            var dataSeed = new DataSeed(context);
            try
            {
                dataSeed.SeedRoles();
                dataSeed.SeedAdmin();
                dataSeed.SeedUsers();
                dataSeed.SeedRegions();
                dataSeed.SeedPostCategories();
                dataSeed.SeedPets();
                dataSeed.SeedPostsWithComments(
                    context.Users.ToList(), 
                    context.PostCategories.ToList(), 
                    context.Pets.ToList(),
                    context.Regions.ToList());
            }
            catch (System.Data.Entity.Core.UpdateException ex)
            {

                throw;
            }
        }
    }
}
