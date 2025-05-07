using Microsoft.EntityFrameworkCore;
using MVC_Application.Data;

namespace MVC_Application.Models.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDBContext bloggieDBContext;
        public BlogPostLikeRepository(BloggieDBContext bloggieDBContext)
        {
            this.bloggieDBContext = bloggieDBContext;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDBContext.BlogPostLike
                .CountAsync(x=>x.BlogPostId == blogPostId);
        }
    }
}
