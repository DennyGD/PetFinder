namespace PetFinder.Web.Areas.Private.ViewModels
{
    using System;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class CommentPrivateViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int PostId { get; set; }

        public bool PostIsDeleted { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentPrivateViewModel>()
                .ForMember(
                x => x.PostIsDeleted,
                opts => opts.MapFrom(x => x.Post.IsDeleted));
        }
    }
}