namespace PetFinder.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class AllPageViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IList<PostBaseViewModel> Posts { get; set; }
    }
}