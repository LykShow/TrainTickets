using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTickets.Models;
using TrainTickets.Models.TrainViewModel;

namespace TrainTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Name = model.Name, SoName= model.SoName, Email = model.Email, UserName = model.Email, Age = model.Age };
               
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                   
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Train", "Train", new { useid = _userManager.Users.Single(i => i.Email == model.Email).Id });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                   
                     
                      return RedirectToAction("Train", "Train" , new { useid = _userManager.Users.Single(i=>i.Email==model.Email).Id });
                    
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
