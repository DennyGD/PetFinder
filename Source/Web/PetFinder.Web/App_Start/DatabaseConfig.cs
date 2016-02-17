namespace PetFinder.Web
{
    using System.Data.Entity;

    using PetFinder.Data;
    using PetFinder.Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
            AppDbContext.Create().Database.Initialize(true);
        }
    }
}