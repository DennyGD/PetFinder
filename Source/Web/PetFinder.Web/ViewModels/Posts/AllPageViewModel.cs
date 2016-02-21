namespace PetFinder.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using Shared;

    public class AllPageViewModel
    {
        public PageInfo PageInfo { get; set; }

        public IList<PostBaseViewModel> Posts { get; set; }
    }
}