namespace PetFinder.Web.ViewModels.Posts
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    // TODO must add more info
    public class PostExtendedViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string User { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, PostExtendedViewModel>()
                .ForMember(
                x => x.User,
                opts => opts.MapFrom(x => x.User.FirstName + " " + x.User.LastName));
        }
    }
}