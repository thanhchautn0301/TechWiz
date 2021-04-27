using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Areas.Admin.Models;

namespace TechWizProject.Areas.Admin.Services
{

    public class RestaurantsServicesImpl : IRestaurantsServies
    {
        private DatabaseContext db;
        public RestaurantsServicesImpl(DatabaseContext databaseContext)
        {
            db = databaseContext;
        }

        public async Task<List<Admin.Models.Restaurant>> FindAll() => await db.Restaurants.Where(r => r.Status == 1).ToListAsync(); 

        public async Task Create([Bind("Id,OwnerId,Name,Addr,Lat,Lng,Cover,Logo,ShippingFeePerKm,Status,CreatedAt,UpdatedAt")] Restaurant restaurant)
        {
            db.Add(restaurant);
            await db.SaveChangesAsync();
        }

        // public async Task<Restaurant> Find(int? id) => await db.Restaurants.Find(id)
        public async Task Edit([Bind("Id,OwnerId,Name,Addr,Lat,Lng,Cover,Logo,ShippingFeePerKm,Status,CreatedAt,UpdatedAt")] Restaurant restaurant)
        {
            db.Entry(restaurant).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<Restaurant> Find(int? id) => await db.Restaurants.FindAsync(id);

        public bool ResExists(int id) => db.Restaurants.Any(e => e.Id == id);

        public async Task Delete(int id)
        {
            Restaurant res = await db.Restaurants.FindAsync(id);
            res.Status = 0;
            await db.SaveChangesAsync();
        }
    }
}
