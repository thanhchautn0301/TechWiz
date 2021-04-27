using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public interface OrderService
    {
        public Task<List<Order>> GetAllOrder();
        public bool OrderExists(int id);
        public Task Create(Order order, List<CartItem> listcart);
        public Task<Order> Find(int? id);
        public Task Update(Order order);
        public Task Cancel(int? id);
        public int GetIdRestaurant(int foodid);
    }
}
