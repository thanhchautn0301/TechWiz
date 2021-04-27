using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public interface IFoodsService
    {
        public Task<List<Food>> LoadFoods();
        public Task<List<Food>> SearchByFoodName(string foodName);
        public Task<List<Food>> SearchByFoodName(string foodName,int categoryId,int restaurantId);
        public Task<List<Food>> FilterByCategories(string keyword, int categoryId);
        public Task<List<Food>> FilterByRestaurants(string keyword, int restaurantId);
        public (List<Food> foods, int amountOfPages, int page) Paging(int page, List<Food> loadFoods);
    }
}
