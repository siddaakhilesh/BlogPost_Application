using Microsoft.EntityFrameworkCore;
using MVC_Application.Models.Domain;

namespace MVC_Application.Data
{
    public class BloggieDBContext : DbContext
    {
        public BloggieDBContext(DbContextOptions options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            //configuring schema for the table
          //  modelBuilder.HasDefaultSchema("Bloggie");

            //Mapping entity to table
            //modelBuilder.Entity<BlogPost>().ToTable("BlogPosts");      
       //}

        //to create table of blogpost columns in database we named it as BlogPosts
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }        
    }
}
