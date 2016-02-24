namespace PetFinder.Web.ViewModels.Posts
{
    using System.Linq;

    using AutoMapper;

    using Data.Models;
    using Images;
    using Infrastructure.Mapping;

    public class PostExtendedViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string User { get; set; }

        public int CommentsCount { get; set; }

        public bool IsSolved { get; set; }

        public string Region { get; set; }

        public string PostCategory { get; set; }

        public string Pet { get; set; }

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

            configuration.CreateMap<Post, PostExtendedViewModel>()
                .ForMember(
                x => x.Region,
                opts => opts.MapFrom(x => x.Region.Name));

            configuration.CreateMap<Post, PostExtendedViewModel>()
                .ForMember(
                x => x.PostCategory,
                opts => opts.MapFrom(x => x.PostCategory.Name));

            configuration.CreateMap<Post, PostExtendedViewModel>()
                .ForMember(
                x => x.Pet,
                opts => opts.MapFrom(x => x.Pet.Name));
        }
    }
}