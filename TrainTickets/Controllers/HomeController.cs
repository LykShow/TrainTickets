using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TrainTickets.Models;

namespace TrainTickets.Controllers
{
    public class HomeController : Controller
    {


        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index(string id)
        {
            var use= HttpContext.User.Identity.Name;
            if (userManager.Users.Count() != 0 && string.IsNullOrEmpty(id))
            {
                var user = userManager.Users.Single(x => x.UserName == use);
                id = user.Id;
            }
                                                      
            ViewBag.UserId = id;
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
