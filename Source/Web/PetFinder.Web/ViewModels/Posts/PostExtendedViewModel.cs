namespace PetFinder.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using AutoMapper;
    using Comments;
    using Data.Models;
    using Infrastructure.Mapping;
    
    // TODO must add more info
    public class PostExtendedViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string User { get; set; }

        public int CommentsCount { get; set; }

        //public IList<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, PostExtendedViewModel>()
                .ForMember(
                x => x.User,
                opts => opts.MapFrom(x => x.User.FirstName + " " + x.User.LastName));

            configuration.CreateMap<Post, PostExtendedViewModel>()
                .ForMember(
                x => x.CommentsCount,
                opts => opts.MapFrom(x => x.Comments.Count));
        }
    }
}