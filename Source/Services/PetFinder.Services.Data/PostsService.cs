namespace PetFinder.Services.Data
{
    using System;
    using System.Linq;

    using Contracts;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;

    public class PostsService : IPostsService
    {
        private readonly IDbRepository<Post> postsRepo;

        public PostsService(IDbRepository<Post> postsRepo)
        {
            this.postsRepo = postsRepo;
        }

        public IQueryable<Post> GetLastByCategory(string category, int count = 5)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return null;
            }

            return this.postsRepo
                .All()
                .Where(x => (x.PostCategory.Name.ToLower() == category.ToLower()) && !x.IsSolved)
                .OrderByDescending(x => x.CreatedOn)
                .Take(count);
        }
    }
}
