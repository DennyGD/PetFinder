namespace PetFinder.Services.Data
{
    using System;
    using System.Linq;

    using Contracts;
    using PetFinder.Data.Common;
    using PetFinder.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IDbRepository<User> usersRepo;

        private readonly IDbRepository<Post> postsRepo;

        private readonly IDbRepository<Comment> commentsRepo;

        public UsersService(IDbRepository<User> usersRepo, IDbRepository<Post> postsRepo, IDbRepository<Comment> commentsRepo)
        {
            this.usersRepo = usersRepo;
            this.postsRepo = postsRepo;
            this.commentsRepo = commentsRepo;
        }

        public IQueryable<User> All(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return this.usersRepo.AllWithDeleted();
            }
            else
            {
                return this.usersRepo.All();
            }
        }

        public User ById(string id, bool includeDeleted)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            if (includeDeleted)
            {
                return this.GetByIdEvenIfDeleted(id);
            }

            return this.usersRepo.GetById(id);
        }

        public bool Update(string email, string firstName, string lastName, bool isDeleted, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var userToUpdate = this.GetByIdEvenIfDeleted(id);
            if (userToUpdate == null)
            {
                return false;
            }

            userToUpdate.Email = email;
            userToUpdate.FirstName = firstName;
            userToUpdate.LastName = lastName;
            userToUpdate.UserName = email;

            try
            {
                if (userToUpdate.IsDeleted != isDeleted)
                {
                    this.HandleChangesInIsDeletedStatus(userToUpdate, isDeleted);
                }

                this.usersRepo.Save();
                return true;
            }
            catch (Exception)
            {
                // TODO log exception in some error logger
                return false;
            }
        }

        private User GetByIdEvenIfDeleted(string id)
        {
            var user = this.usersRepo.AllWithDeleted().Where(x => x.Id == id).FirstOrDefault();
            return user;
        }

        private void HandleChangesInIsDeletedStatus(User user, bool isDeleted)
        {
            if (isDeleted)
            {
                this.DeletePostsAndComments(user);
                this.usersRepo.Delete(user);
            }
            else
            {
                // user has been deleted but now is turned back to life, so status for posts and comments should be changed, too
                this.UndoDeletePostsAndComments(user);
                user.IsDeleted = isDeleted;
            }
        }

        private void DeletePostsAndComments(User user)
        {
            foreach (var post in user.Posts)
            {
                this.postsRepo.Delete(post);
            }

            this.postsRepo.Save();

            foreach (var comment in user.Comments)
            {
                this.commentsRepo.Delete(comment);
            }

            this.commentsRepo.Save();
        }

        private void UndoDeletePostsAndComments(User user)
        {
            foreach (var post in user.Posts)
            {
                post.IsDeleted = false;
            }

            this.postsRepo.Save();

            foreach (var comment in user.Comments)
            {
                comment.IsDeleted = false;
            }

            this.commentsRepo.Save();
        }
    }
}
