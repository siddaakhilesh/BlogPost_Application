﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MVC_Application.Data;
using MVC_Application.Models.Domain;

namespace MVC_Application.Models.Repositories
{
    public class TagRepository :ITagRepository
    {
        private readonly BloggieDBContext bloggieDbContext;
        public TagRepository(BloggieDBContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await bloggieDbContext.Tags.ToListAsync();
        }
        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        
        }
        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(id);
            if(existingTag != null)
            {
                bloggieDbContext.Tags.Remove(existingTag);
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
        public async Task<Tag?> GetAsync(Guid id)
        {
            return await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
