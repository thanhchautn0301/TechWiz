using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public class CategoryServiceImpl : CategoryService
    {
        private readonly DatabaseContext _context;
        public CategoryServiceImpl(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Create(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await Find(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, [Bind(new[] { "Id,Name,Description,Icon,Status,CreatedAt,UpdatedAt" })] Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> Find(int? id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task<Category> Details(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m=>m.Id==id);
        }

        public async Task<List<Category>> List()
        {
           return await _context.Categories.Where(m=>m.Status==1).ToListAsync();
        }

        public Task<Category> FindId(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
