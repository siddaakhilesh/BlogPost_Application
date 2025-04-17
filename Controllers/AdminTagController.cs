using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MVC_Application.Data;
using MVC_Application.Models.Domain;
using MVC_Application.Models.Repositories;
using MVC_Application.Models.ViewModels;
using System.Security.Cryptography.Xml;

namespace MVC_Application.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepository tagRepository;
        public AdminTagController(ITagRepository tagRepository)
        {
                this.tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View("Admin");
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> SubmitTag(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag Domain Model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            await tagRepository.AddAsync(tag);
            
            //var name = addTagRequest.Name;
            //var displayName = addTagRequest.DisplayName;

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //use DBContext to read the tags
            //var tags = await bloggieDbContext.Tags.ToListAsync();

            //pass the tags to the view

            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st Method
            //var tag = bloggieDbContext.Tags.Find(id);

            //2nd Method - Using Linq Query
            //var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

            var tag = await tagRepository.GetAsync(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            //var existingTag =await bloggieDbContext.Tags.FindAsync(tag.Id);
            //if (existingTag != null)
            //{
            //  existingTag.Name = tag.Name;
            // existingTag.DisplayName = tag.DisplayName;

            //save changes
            // bloggieDbContext.SaveChanges();
            // return RedirectToAction("List");
            // }

            var updatedTag= await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                //show notification
                return RedirectToAction("List");
            }
            else
            {
                //show error notification
            }
            return RedirectToAction("Edit", new { id = tag.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            //var tag= await bloggieDbContext.Tags.FindAsync(editTagRequest.Id);
            //if (tag != null)
            //{
            //  bloggieDbContext.Tags.Remove(tag);
            //bloggieDbContext.SaveChanges();

            //show a success notification
            //return RedirectToAction("List");
            //}

            var deletedTag= await tagRepository.DeleteAsync(editTagRequest.Id);
            if(deletedTag!=null)
            {
                //Show success notification
                return RedirectToAction("List");
            }
            //show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
