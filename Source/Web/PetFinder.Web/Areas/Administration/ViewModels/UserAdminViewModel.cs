namespace PetFinder.Web.Areas.Administration.ViewModels
{
    using System;

    using Data.Models;
    using Infrastructure.Mapping;

    public class UserAdminViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string UserName { get; set; }
    }
}