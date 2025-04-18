using Microsoft.EntityFrameworkCore;
using MVC_Application.Data;
using MVC_Application.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Injecting db context class into our application
builder.Services.AddDbContext<BloggieDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnection")));

//INJECTING TAG REPOSITORY
builder.Services.AddScoped<ITagRepository, TagRepository>();

//INJECTING BLOGPOST REPOSITORY
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
