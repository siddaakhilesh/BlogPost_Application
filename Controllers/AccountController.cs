using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using MVC_Application.Models.ViewModels;

namespace MVC_Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
                this.userManager = userManager;
               this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if(identityResult.Succeeded)
            {
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                if(roleIdentityResult.Succeeded)
                {
                    return RedirectToAction("Register");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new loginViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(loginViewModel loginViewModel)
        {
           var signInResult= await signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            //show the errors
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");   
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
