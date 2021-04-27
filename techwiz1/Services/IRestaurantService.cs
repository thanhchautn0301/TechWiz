using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public interface IRestaurantService
    {
        public Task<List<Restaurant>> LoadRestaurant();
    }
}
