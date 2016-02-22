namespace PetFinder.Web.ViewModels.Images
{
    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class ImageViewModel : IMapFrom<Image>, IHaveCustomMappings
    {
        public byte[] Content { get; set; }

        public string Extension { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Image, ImageViewModel>()
                .ForMember(
                x => x.Extension,
                opts => opts.MapFrom(x => "image/" + x.FileExtension));
        }
    }
}