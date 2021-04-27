using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Areas.Admin.Models;
namespace TechWizProject.Areas.Admin.Services
{
    public interface IRestaurantsServies
    {
        Task<List<Restaurant>> FindAll();
        Task Create([Bind("Id,OwnerId,Name,Addr,Lat,Lng,Cover,Logo,ShippingFeePerKm,Status,CreatedAt,UpdatedAt")] Restaurant restaurant);

        Task Edit([Bind("Id,OwnerId,Name,Addr,Lat,Lng,Cover,Logo,ShippingFeePerKm,Status,CreatedAt,UpdatedAt")] Restaurant restaurant);

        Task<Restaurant> Find(int? id);
        bool ResExists(int id);
        Task Delete(int id);
    }
}
