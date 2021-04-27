using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Areas.Users.Services
{
    public interface IOrderService
    {
        public List<Order> LoadOrder();
        public List<Order> SearchOrder(int orderId);

    }
}
