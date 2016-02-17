namespace PetFinder.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using PetFinder.Common.Constants;
    using Models;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    public class DataSeed
    {
        private const string DefaultPassword = "123456";

        private const string DefaultUserName = "someUser";

        private IList<string> regions = new List<string>()
        {   "Благоевград",
            "Бургас",
            "Варна",
            "Велико Търново",
            "Видин",
            "Враца ",
            "Габрово",
            "Добрич",
            "Кърджали",
            "Кюстендил",
            "Ловеч",
            "Монтана",
            "Пазарджик",
            "Плевен",
            "Перник",
            "Пловдив",
            "Разград",
            "Русе",
            "Силистра",
            "Сливен",
            "Смолян",
            "София",
            "Стара Загора",
            "Търговище",
            "Хасково",
            "Шумен",
            "Ямбол"
        };

        private static Random rand = new Random();

        private readonly AppDbContext context;

        public DataSeed(AppDbContext context)
        {
            this.context = context;
        }

        public void SeedRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(this.context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var adminRole = new IdentityRole(Models.AdminRole);
            roleManager.Create(adminRole);
            var specialRole = new IdentityRole(Models.SpecialRole);
            roleManager.Create(specialRole);
        }

        public void SeedAdmin()
        {
            var userManager = new UserManager<User>(new UserStore<User>(this.context));

            //userManager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = false,
            //    RequireLowercase = false,
            //    RequireUppercase = false,
            //};

            var user = new User()
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                FirstName = "Master",
                LastName = "User",
                CreatedOn = DateTime.Now
            };

            userManager.Create(user, DefaultPassword);
            userManager.AddToRole(user.Id, Models.AdminRole);
        }

        public void SeedUsers()
        {
            var userManager = new UserManager<User>(new UserStore<User>(this.context));

            //userManager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = false,
            //    RequireLowercase = false,
            //    RequireUppercase = false,
            //};

            for (int i = 0; i < 5; i++)
            {
                var currentUser = $"{DefaultUserName}{i}";

                var user = new User()
                {
                    Email = currentUser + "@site.com",
                    UserName = currentUser + "@site.com",
                    FirstName = currentUser,
                    LastName = currentUser,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, DefaultPassword);
            }
        }

        public void SeedRegions()
        {
            foreach (var region in this.regions)
            {
                this.context.Regions.Add(new Region() { Name = region });
            }

            this.context.SaveChanges();
            //SaveChanges(this.context);
        }

        public void SeedPostCategories()
        {
            this.context.PostCategories.Add(new PostCategory() { Name = "Изгубени" });
            this.context.PostCategories.Add(new PostCategory() { Name = "Намерени" });
            this.context.PostCategories.Add(new PostCategory() { Name = "Други" });
            this.context.SaveChanges();
            //SaveChanges(this.context);
        }

        public void SeedPets()
        {
            this.context.Pets.Add(new Pet() { Name = "Кучета" });
            this.context.Pets.Add(new Pet() { Name = "Котки" });
            this.context.Pets.Add(new Pet() { Name = "Други" });
            this.context.SaveChanges();
            //SaveChanges(this.context);
        }

        public void SeedPostsWithComments(List<User> users, List<PostCategory> categories, List<Pet> pets, List<Region> regions)
        {
            for (int i = 0; i < 10; i++)
            {
                var post = new Post
                {
                    Title = $"Post title {i}",
                    Content = $"Some Post content... {i}",
                    User = users[rand.Next(0, users.Count)],
                    PostCategory = categories[rand.Next(0, categories.Count)],
                    Pet = pets[rand.Next(0, pets.Count)],
                    EventTime = DateTime.Now.AddDays(-(rand.Next(0, 15))),
                    Region = regions[rand.Next(0, regions.Count)]
                };

                var commentsCount = rand.Next(0, 10);
                for (int j = 0; j < commentsCount; j++)
                {
                    var comment = new Comment
                    {
                        Content = $"Comment content {j}",
                        User = users[rand.Next(0, users.Count)],
                        Post = post
                    };

                    post.Comments.Add(comment);
                }

                this.context.Posts.Add(post);
            }

            this.context.SaveChanges();
            
            //SaveChanges(this.context);
        }

        // ! Error Finder only! Suitable when database is updated from console.
        //private void SaveChanges(DbContext context)
        //{
        //    try
        //    {
        //        context.SaveChanges();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        foreach (var failure in ex.EntityValidationErrors)
        //        {
        //            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
        //            foreach (var error in failure.ValidationErrors)
        //            {
        //                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
        //                sb.AppendLine();
        //            }
        //        }

        //        throw new DbEntityValidationException(
        //            "Entity Validation Failed - errors follow:\n" +
        //            sb.ToString(), ex
        //        ); // Add the original exception as the innerException
        //    }
        //}
    }
}
