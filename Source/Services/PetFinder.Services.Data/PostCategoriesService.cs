namespace PetFinder.Services.Data
{
    using System.Linq;

    using Contracts;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;

    public class PostCategoriesService : IPostCategoriesService
    {
        private readonly IDbRepository<PostCategory> postCategoriesRepo;

        public PostCategoriesService(IDbRepository<PostCategory> postCategoriesRepo)
        {
            this.postCategoriesRepo = postCategoriesRepo;
        }

        public IQueryable<PostCategory> All(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.postCategoriesRepo.AllWithDeleted();
            }
            else
            {
                return this.postCategoriesRepo.All();
            }
        }

        public PostCategory ById(int id, bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.postCategoriesRepo.AllWithDeleted().Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                return this.postCategoriesRepo.GetById(id);
            }
        }
    }
}
