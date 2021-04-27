using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public class CategoriesService : ICategoriesService
    {
        private DatabaseContext db;
        public CategoriesService(DatabaseContext _db)
        {
            db = _db;
        }
        public async Task<List<Category>> LoadCategories()
        {
            return await db.Categories.ToListAsync();
        }
    }
}
