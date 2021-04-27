using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TechWizProject.Models;
using TechWizProject.Services;


namespace TechWizProject.Controllers
{
    [Route("User")]
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;
        private IUserService userService;
        public UsersController(DatabaseContext context, IUserService _userService)
        {
            _context = context;
            userService = _userService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Id,Email,FbId,GgId,Password,Salt,LastName,FirstName,Phone,Role,Avatar,Status,CreatedAt,UpdatedAt")] User user)
        {
            if (!ModelState.IsValid || await userService.EmailExists(user.Email) != null) return View(user);
            var salt = ServiceCommon.ServiceCommon.RandomString();
            user.CreatedAt = DateTime.Now;
            user.Salt = salt;
            user.Role = "customer";
            user.Status = 1;
            user.Password = ServiceCommon.ServiceCommon.ComputeSha256Hash(user.Password, salt);
            await userService.Create(user);
            return Redirect("/home/login");

        }

        [Route("Edit")]
        public async Task<IActionResult> Edit()
        {
            var user = await userService.Find(HttpContext.Session.GetInt32("id"));
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind("Id,Email,FbId,GgId,Password,Salt,LastName,FirstName,Phone,Role,Avatar,Status,CreatedAt,UpdatedAt")]
            User user)
        {
            try
            {
                user.UpdatedAt = DateTime.Now;
                await userService.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (userService.Find(user.Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Redirect("/home");
        }

        private string ComputeSha256Hash(string text)
        {
            using SHA256 sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
