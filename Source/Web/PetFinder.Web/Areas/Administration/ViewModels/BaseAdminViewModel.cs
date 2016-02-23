namespace PetFinder.Web.Areas.Administration.ViewModels
{
    using System;

    public abstract class BaseAdminViewModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}