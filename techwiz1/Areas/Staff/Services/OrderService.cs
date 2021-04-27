using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Areas.Users.Services
{
    public class OrderService : IOrderService
    {
        private DatabaseContext db;
        public OrderService(DatabaseContext _db)
        {
            db = _db;
        }
        public List<Order> LoadOrder()
        {
            return db.Orders.ToList();
        }

        public List<Order> SearchOrder(int orderId)
        {
            return db.Orders.Where(o => o.Id.ToString().ToLower().Contains(orderId.ToString().ToLower())).ToList();
        }
    }
}
