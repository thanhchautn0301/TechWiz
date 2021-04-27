using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public class RestaurantService : IRestaurantService
    {
        private DatabaseContext db;
        public RestaurantService(DatabaseContext _db)
        {
            db = _db;
        }

        public async Task<List<Restaurant>> LoadRestaurant()
        {
            return await db.Restaurants.ToListAsync();
        }
    }
}
