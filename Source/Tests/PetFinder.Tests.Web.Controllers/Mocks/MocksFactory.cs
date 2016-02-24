namespace PetFinder.Tests.Web.Controllers.Mocks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Data.Models;
    using Moq;
    using Services.Data.Contracts;

    public static class MocksFactory
    {
        public static IPostsService GetPostsService()
        {
            var postsService = new Mock<IPostsService>();

            postsService.Setup(x => x.LastByCategory(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new List<Post>().AsQueryable());

            postsService.Setup(x => x.ById(It.Is<int>(i => i == 1)))
                .Returns(new Post()
                {
                    Title = "Test title",
                    Content = "Test Content",
                    PetId = 1,
                    PostCategoryId = 1,
                    RegionId = 1,
                    UserId = "userId",
                    Id = 1
                });

            postsService.Setup(x => x.ById(It.Is<int>(i => i == 0)))
                .Returns((Post)null);

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
