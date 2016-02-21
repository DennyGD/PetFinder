namespace PetFinder.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using Comments;

    public class DetailsPageViewModel
    {
        public PostExtendedViewModel MainInfo { get; set; }

        public IList<CommentViewModel> Comments { get; set; }
    }
}