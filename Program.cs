using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Application.Data;
using MVC_Application.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Injecting db context class into our application
builder.Services.AddDbContext<BloggieDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnection")));

//Injecting AuthDb COntext into our application
builder.Services.AddDbContext<AuthDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieAuthDbConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

//setting how password should be given 
builder.Services.Configure<IdentityOptions>(options =>
{
    //default settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

//INJECTING TAG REPOSITORY
builder.Services.AddScoped<ITagRepository, TagRepository>();

//INJECTING BLOGPOST REPOSITORY
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

//inject images repository
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

//injecting the bloggiepost like repository into our application
builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
