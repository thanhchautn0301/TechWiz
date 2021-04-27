using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;
using TechWizProject.ViewModels;

namespace TechWizProject.Services
{
    public interface OrderTrackingService
    {
        public Task<ListOrderTrackingAndListOrderDetail> GetAllOrderTrackingAndAllElementInOrderDetailsById(int id);
        public Task<OrderTracking> GetOrderTrackingDetails(int? id);
        public Task AcceptOrder(int id);
        public Task<List<OrderDetailsViewModel>> GetAllElementById(int idOrder);
    }
}
