using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public interface FoodService
    {
        public Task<List<Food>> GetAllFood();
        public Task<Food> GetFoodDetail(int? id);
        public Task Create(Food food);
        public Task<Food> Edit(int? id);
        public Task Update(Food food);
        public Task Delete(Food food);
        public Task<Food> Find(int id);
        public bool FoodExists(int id);

        public Task<List<string>> GetRestaurant();
        public Task<List<string>> GetCategory();
        public Task<string> GetRestaurantAndCategory(int? id, int? restaurantId, int? categoryId);
        
    }
}
