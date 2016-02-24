namespace PetFinder.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using PetFinder.Data.Models;

    public interface IPostsService
    {
        IQueryable<Post> LastByCategory(string category, int count = 5);

        IQueryable<Post> All(bool includeDeleted);

        IQueryable<Post> All(bool isSolved, string category = "");

        IQueryable<Post> All(int page, int pageSize, string region);

        Post ById(int id);

        int AllPostsCount(string region);

        Post Add(string title, string content, DateTime eventTime, int regionId, int postCategoryId, int petId, string userId, IEnumerable<HttpPostedFileBase> files);

        void Update(string title, string content, bool isDeleted, int id);

        void HardDelete(int id);
    }
}
