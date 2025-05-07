using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Application.Data;
using MVC_Application.Models.Domain;
using MVC_Application.Models.ViewModels;
using MVC_Application.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

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

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var exisitingBlog =await bloggieDbCOntext.BlogPosts.FindAsync(id);
            if(exisitingBlog!=null)
            {
                bloggieDbCOntext.BlogPosts.Remove(exisitingBlog);
                await bloggieDbCOntext.SaveChangesAsync();
                return exisitingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbCOntext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggieDbCOntext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await bloggieDbCOntext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.UrlHandle== urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await bloggieDbCOntext.BlogPosts.FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if(existingBlog!=null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle=blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                await bloggieDbCOntext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
