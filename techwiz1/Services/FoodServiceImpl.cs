using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public class FoodServiceImpl : FoodService
    {
        private DatabaseContext db;

        public FoodServiceImpl(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task Create(Food food)
        {
            db.Add(food);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Food food)
        {
            db.Foods.Remove(food);
            await db.SaveChangesAsync();
        }

        public async Task<Food> Edit(int? id)
        {
            return await db.Foods.FindAsync(id);
        }

        public async Task<Food> Find(int id)
        {
            return await db.Foods.FindAsync(id);
        }

        public bool FoodExists(int id)
        {
            return db.Foods.Any(e => e.Id == id);
        }

        public async Task<List<Food>> GetAllFood()
        {
            return await db.Foods.Where(p => p.Status != 0).ToListAsync();
        }

        public async Task<List<string>> GetCategory()
        {
            var categories = await db.Categories.Where(p => p.Status != 0).ToListAsync();
            return categories.Select(item => item.Id + "-" + item.Name).ToList();
        }

        public async Task<Food> GetFoodDetail(int? id)
        {
            return await db.Foods
                  .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<string>> GetRestaurant()
        {
            var restaurants = await db.Restaurants.Where(p => p.Status != 0).ToListAsync();
            return restaurants.Select(item => item.Id + "-" + item.Name).ToList();
        }

        public async Task<string> GetRestaurantAndCategory(int? id, int? restaurantId, int? categoryId)
        {
            var restaurants = await db.Restaurants.SingleOrDefaultAsync(p => p.Status != 0 && p.Id == restaurantId);
            var categories = await db.Categories.SingleOrDefaultAsync(p => p.Status != 0 && p.Id == categoryId);
            return restaurants.Name + "-" + categories.Name;
        }

        public async Task Update(Food food)
        {
            db.Update(food);
            await db.SaveChangesAsync();
        }
    }
}
