namespace PetFinder.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using AutoMapper;
    using Comments;
    using Data.Models;
    using Infrastructure.Mapping;
    using Images;
    using System.Linq;    // TODO must add more info
    public class PostExtendedViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string User { get; set; }

        public int CommentsCount { get; set; }

        public bool IsSolved { get; set; }

        public ImageViewModel Image { get; set; }

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

            configuration.CreateMap<Post, PostExtendedViewModel>()
                .ForMember(
                x => x.Image,
                opts => opts.MapFrom(x => x.Images.FirstOrDefault()));
        }
    }
}