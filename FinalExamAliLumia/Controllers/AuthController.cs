using FinalExamAliLumia.Models;
using FinalExamAliLumia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamAliLumia.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            AppUser user = new AppUser()
            {  
                Name =registerVM.FirstName,
                Surname = registerVM.LastName,
                UserName = registerVM.UserName,
                Email = registerVM.Email
            };
            IdentityResult identityResult =await _userManager.CreateAsync(user,registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
             await _signInManager.SignInAsync(user,true);

            return RedirectToAction("Index","Home");

        }
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Login(LoginVm loginVm)
        {

            AppUser user;
            if (loginVm.UserNameOrEmail.Contains("@"))
            {
                user =await _userManager.FindByEmailAsync(loginVm.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginVm.UserNameOrEmail);
            }           
            if(user == null)
            {
                ModelState.AddModelError("","Sehvlik var!!");
                return View(loginVm);
            }
            var result =await _signInManager.PasswordSignInAsync(user,loginVm.Password,true,false);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Muveqqeti blok!!");
                return View(loginVm);
            }
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Sehvlik var!!");
                return View(loginVm);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
