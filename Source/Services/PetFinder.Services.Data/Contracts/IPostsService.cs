﻿namespace PetFinder.Services.Data.Contracts
{
    using System.Linq;

    using PetFinder.Data.Models;

    public interface IPostsService
    {
        IQueryable<Post> LastByCategory(string category, int count = 5);

        IQueryable<Post> All(bool isSolved, string category = "");
    }
}
