using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public class FoodsService : IFoodsService
    {
        private DatabaseContext db;


        public FoodsService(DatabaseContext _db)
        {
            db = _db;
        }
        public async Task<List<Food>> LoadFoods()
        {
            return await db.Foods.OrderByDescending(f => f.Id).ToListAsync();
        }

        public async Task<List<Food>> SearchByFoodName(string foodName)
        {
            if (foodName == null)
            {

                return await db.Foods.OrderByDescending(f => f.Id).ToListAsync();
            }
            else
            {
                return await db.Foods.Where(f => f.Name.ToLower().Contains(foodName.ToLower())).OrderByDescending(f => f.Id).ToListAsync();
            }
        }

        public (List<Food> foods, int amountOfPages, int page) Paging(int page, List<Food> loadFoods)
        {
            var LastIndex = (page - 1) * 5;
            var amountOfPages = (int)Math.Ceiling((Decimal)loadFoods.Count() / 5);
            var foods = loadFoods.Skip(LastIndex).Take(5).ToList();
            return (foods, amountOfPages, page);
        }


        public async Task<List<Food>> SearchByFoodName(string foodName, int categoryId, int restaurantId)
        {
            if (foodName != null)
            {
                return await db.Foods.Where(f => f.Name.ToLower().Contains(foodName.ToLower()) && f.CategoryId == categoryId && f.RestaurantId == restaurantId).ToListAsync();
            }
            return await db.Foods.Where(f => f.CategoryId == categoryId && f.RestaurantId == restaurantId).ToListAsync();

        }

        public async Task<List<Food>> FilterByCategories(string keyword, int categoryId)
        {
            if (keyword != null)
            {
                return await db.Foods.Where(f => f.CategoryId == categoryId && f.Name.ToLower().Contains(keyword.ToLower())).ToListAsync();
            }
            return await db.Foods.Where(f => f.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Food>> FilterByRestaurants(string keyword, int restaurantId)
        {
            if (keyword != null)
            {
                return await db.Foods.Where(f => f.RestaurantId == restaurantId && f.Name.ToLower().Contains(keyword.ToLower())).ToListAsync();
            }
            return await db.Foods.Where(f => f.RestaurantId == restaurantId).ToListAsync();
        }
    }
}
