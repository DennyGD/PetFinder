namespace PetFinder.Services.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

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

        public IQueryable<Post> LastByCategory(string category, int count = 5)
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

        public IQueryable<Post> All(bool isSolved, string category = "")
        {
            Expression<Func<Post, bool>> query;
            if (string.IsNullOrWhiteSpace(category))
            {
                query = x => x.IsSolved == isSolved;
            }
            else
            {
                query = x => (x.IsSolved == isSolved) && (x.PostCategory.Name.ToLower() == category.ToLower());
            }

            return this.postsRepo
                .All()
                .Where(query)
                .OrderByDescending(x => x.CreatedOn);
        }
    }
}
