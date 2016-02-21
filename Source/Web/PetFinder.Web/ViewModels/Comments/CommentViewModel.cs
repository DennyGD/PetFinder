namespace PetFinder.Web.ViewModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using PetFinder.Data.Models;
    using PetFinder.Web.Infrastructure.Mapping;
    
    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string User { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(
                x => x.User,
                opts => opts.MapFrom(x => x.User.FirstName + " " + x.User.LastName));
        }
    }
}