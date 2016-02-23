namespace PetFinder.Services.Data
{
    using System;
    using System.Linq;

    using Contracts;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;

    public class PetsService : IPetsService
    {
        private readonly IDbRepository<Pet> petsRepo;

        private readonly IDbRepository<Comment> commentsRepo;

        private readonly IDbRepository<Post> postsRepo;

        public PetsService(IDbRepository<Pet> petsRepo, IDbRepository<Comment> commentsRepo, IDbRepository<Post> postsRepo)
        {
            this.petsRepo = petsRepo;
            this.commentsRepo = commentsRepo;
            this.postsRepo = postsRepo;
        }

        public IQueryable<Pet> All(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.petsRepo.AllWithDeleted();
            }
            else
            {
                return this.petsRepo.All();
            }
        }

        public Pet ById(int id, bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.petsRepo.AllWithDeleted().Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                return this.petsRepo.GetById(id);
            }
        }

        public Pet Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var pet = new Pet() { Name = name };
            this.petsRepo.Add(pet);
            try
            {
                this.petsRepo.Save();
                return this.petsRepo.All().Where(x => x.Name == name).FirstOrDefault();
            }
            catch (Exception)
            {
                // log
                return null;
            }
        }

        public void Update(string name, bool isDeleted, int id)
        {
            var petToUpdate = this.ById(id, true);
            if (petToUpdate == null || string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            petToUpdate.Name = name;
            if (petToUpdate.IsDeleted != isDeleted)
            {
                this.HandleChangesInIsDeletedStatus(petToUpdate, isDeleted);
            }

            try
            {
                this.petsRepo.Save();
            }
            catch (Exception)
            {
                // log
            }
        }

        private void HandleChangesInIsDeletedStatus(Pet pet, bool isDeleted)
        {
            if (isDeleted == true)
            {
                this.Delete(pet);
                this.petsRepo.Delete(pet);
            }
            else
            {
                this.UndoDelete(pet);
                pet.IsDeleted = false;
            }
        }

        private void Delete(Pet pet)
        {
            foreach (var post in pet.Posts)
            {
                foreach (var comment in post.Comments)
                {
                    this.commentsRepo.Delete(comment);
                }

                this.commentsRepo.Save();
                this.postsRepo.Delete(post);
            }

            this.postsRepo.Save();
        }

        private void UndoDelete(Pet pet)
        {
            foreach (var post in pet.Posts)
            {
                foreach (var comment in post.Comments)
                {
                    comment.IsDeleted = false;
                }

                this.commentsRepo.Save();

                post.IsDeleted = false;
            }

            this.postsRepo.Save();
        }
    }
}
