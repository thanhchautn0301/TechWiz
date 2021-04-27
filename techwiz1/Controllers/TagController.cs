using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechWizProject.Models;
using TechWizProject.Services;

namespace TechWizProject.Controllers
{[Route("tag")]
    public class TagController : Controller
    {
        private readonly TagService tagService;
        private readonly DatabaseContext _context;

        public TagController(TagService _tagService, DatabaseContext context)
        {
            tagService = _tagService;
            _context = context;
        }
        
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            return View(await tagService.List());
        }

        [Route("create")]
        public IActionResult Create()
        {
            return View("create");
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTag,NameTag,Status")] Tag tag)
        {
            if (!ModelState.IsValid) return View(tag);
            await tagService.Create(tag);
            return RedirectToAction(nameof(Index));
        }

        [Route("edit/{if}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTag,NameTag,Status")] Tag tag)
        {
            if (id != tag.IdTag)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(tag);
            try
            {
                await tagService.Edit(id,tag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(tag.IdTag))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Route("delete{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await tagService.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            tag.Status = 0;
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.IdTag == id);
        }
    }
}
