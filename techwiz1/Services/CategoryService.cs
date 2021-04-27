using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public interface CategoryService
    {
        Task<List<Category>> List();
        Task<Category> Find(int? id);
        Task Create(Category category);
        Task Edit(int id, [Bind("Id,Name,Description,Icon,Status,CreatedAt,UpdatedAt")] Category category);
        Task Delete(int id);
        Task<Category> Details(int? id);
    }
}
