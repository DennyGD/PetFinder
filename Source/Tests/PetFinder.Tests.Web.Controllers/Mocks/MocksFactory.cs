namespace PetFinder.Tests.Web.Controllers.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using Services.Data.Contracts;
    using Data.Models;
    using System.Web;
    public static class MocksFactory
    {
        //public static ICategoriesService GetCategoriesService()
        //{
        //    var categoriesService = new Mock<ICategoriesService>();

        //    categoriesService.Setup(x => x.All()).Returns(categories);
        //    categoriesService.Setup(x => x.ByName(It.Is<string>(n => n == "Valid"))).Returns(categories);
        //    categoriesService.Setup(x => x.ByName(It.Is<string>(n => n == "Invalid"))).Returns(new List<Category>().AsQueryable());
        //    categoriesService.Setup(x => x.Add(It.IsAny<string>())).Returns(1);

        //    return categoriesService.Object;
        //}

        public static IPostsService GetPostsService()
        {
            var postsService = new Mock<IPostsService>();

            postsService.Setup(x => x.LastByCategory(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new List<Post>().AsQueryable());

            return postsService.Object;
        }

        public static ICommentsService GetCommentsService()
        {
            var commentsService = new Mock<ICommentsService>();

            commentsService.Setup(x => x.AllByPostId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Comment>().AsQueryable());

            return commentsService.Object;
        }

        public static HttpContextBase GetHttpContext()
        {
            var httpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();

            httpContext
                .SetupGet(x => x.Response)
                .Returns(response.Object);

            return httpContext.Object;
        }
    }
}
