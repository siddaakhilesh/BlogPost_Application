using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Application.Data;
using MVC_Application.Models.Domain;
using MVC_Application.Models.ViewModels;
using MVC_Application.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MVC_Application.Models.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDBContext bloggieDbCOntext;
        public BlogPostRepository(BloggieDBContext bloggieDbCOntext)
        {
            this.bloggieDbCOntext = bloggieDbCOntext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggieDbCOntext.AddAsync(blogPost);
            await bloggieDbCOntext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbCOntext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public Task<BlogPost?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
