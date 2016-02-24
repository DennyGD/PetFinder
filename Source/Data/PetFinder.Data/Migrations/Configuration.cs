namespace PetFinder.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var dataSeed = new DataSeed(context);            
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
            dataSeed.SeedImages(context.Posts.ToList());
        }
    }
}
