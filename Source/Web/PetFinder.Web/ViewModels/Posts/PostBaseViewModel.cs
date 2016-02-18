namespace PetFinder.Web.ViewModels.Posts
{
    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class PostBaseViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        private const int ContentDisplayMaxLength = 60;

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UrlTitle
        {
            get { return this.Title.Replace(' ', '-'); }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, PostBaseViewModel>()
                .ForMember(
                x => x.Content,
                opts => opts.MapFrom(x => x.Content.Length > ContentDisplayMaxLength ? x.Content.Substring(0, ContentDisplayMaxLength) + "..." : x.Content));
        }
    }
}