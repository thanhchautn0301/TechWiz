
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Services;

namespace TechWizProject.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private IUserService userService;
        public HomeController(IUserService _userService)
        {
            userService = _userService;
        }

        [Route("")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View("index");
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await userService.EmailExists(username);
            if (user == null) return View();
            if (user.Password != ServiceCommon.ServiceCommon.ComputeSha256Hash(password, user.Salt)) return View();
            HttpContext.Session.SetString("username", user.FirstName + user.LastName);
            HttpContext.Session.SetInt32("id", user.Id);
            return user.Role switch
            {
                "customer" => RedirectToAction("index"),
                "admin" => Redirect("/admin/home/index"),
                "staff" => RedirectToAction("Login"),
                _ => RedirectToAction("Login")
            };
        }
    }
}

