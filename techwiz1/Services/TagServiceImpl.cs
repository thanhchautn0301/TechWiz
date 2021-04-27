using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public class TagServiceImpl : TagService
    {
        private DatabaseContext _context;
        public TagServiceImpl(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Create(Tag tag)
        {
            _context.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tag = await Find(id);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<Tag> Details(int? id)
        {
            return await _context.Tags.FirstOrDefaultAsync(m => m.IdTag == id);
        }

        public async Task Edit(int id, [Bind(new[] { "IdTag,NameTag,Status" })] Tag tag)
        {
            _context.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<Tag> Find(int? id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<List<Tag>> List()
        {
            return await _context.Tags.Where(m => m.Status == 1).ToListAsync();
        }
    }
}
