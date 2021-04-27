using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechWizProject.Areas.Admin.Models;
using TechWizProject.Areas.Admin.Services;

namespace TechWizProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/user")]
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;
        private IUserServices userServices;
        public UsersController(IUserServices _userServices)
        {
            userServices = _userServices;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.listUser = await userServices.FindAll();
            return View();
        }

        // GET: Admin/Users/Details/5


        [Route("Create")]
        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,FbId,GgId,Password,Salt,LastName,FirstName,Phone,Role,Avatar,Status,CreatedAt,UpdatedAt")] User user)
        {
            if (ModelState.IsValid && await userServices.EmailExists(user.Email) == null)
            {
                var salt = ServiceCommon.ServiceCommon.RandomString();
                user.CreatedAt = DateTime.Now;
                user.Salt = salt;
                user.Status = 1;
                user.Password = ServiceCommon.ServiceCommon.ComputeSha256Hash(user.Password, salt);
                await userServices.Create(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
