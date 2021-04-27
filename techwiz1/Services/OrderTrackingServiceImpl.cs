using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;
using TechWizProject.ViewModels;

namespace TechWizProject.Services
{
    public class OrderTrackingServiceImpl : OrderTrackingService
    {
        private DatabaseContext db;

        public OrderTrackingServiceImpl(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task AcceptOrder(int id)
        {
            var c = (from x in db.OrderTrackings
                     where x.OrderId == id && x.State == "Accepted"
                     select x).First();
            c.UpdatedAt = DateTime.Now;
            c.Status = 1;
            await db.SaveChangesAsync();
        }

        public async Task<List<OrderDetailsViewModel>> GetAllElementById(int idOrder)
        {
             var listOrderDetail = new List<OrderDetailsViewModel>();

        var orderdetails = await db.OrderDetails.Where(p => p.OrderId == idOrder).ToListAsync();
            foreach (var orderdetail in orderdetails)
            {
                var orderDetail = new OrderDetailsViewModel();
                var foods = await db.Foods.SingleOrDefaultAsync(p => p.Id == orderdetail.FoodId);
                {

                    orderDetail.Image = foods.Images;
                    orderDetail.FoodName = foods.Name;
                    orderDetail.Price = foods.Price;
                    orderDetail.Quantity = orderdetail.Quantity;
                    orderDetail.Discount = (double)orderdetail.Discount;
                    orderDetail.DateBuy = (DateTime)orderdetail.CreatedAt;
                    
                    listOrderDetail.Add(orderDetail);

                };
            }

            return listOrderDetail;

        }



        public async Task<ListOrderTrackingAndListOrderDetail> GetAllOrderTrackingAndAllElementInOrderDetailsById(int id)
        {

            var listOrderDetail = new List<OrderDetailsViewModel>();

            var listOrderTracking = await db.OrderTrackings.Where(p => p.OrderId == id && p.State != "Cancel").ToListAsync();
            var orderdetails = await db.OrderDetails.Where(p => p.OrderId == id).ToListAsync();
            foreach (var orderdetail in orderdetails)
            {
                var orderDetail = new OrderDetailsViewModel();
                var foods = await db.Foods.SingleOrDefaultAsync(p => p.Id == orderdetail.FoodId);
                {

                    orderDetail.Image = foods.Images;
                    orderDetail.FoodName = foods.Name;
                    orderDetail.Price = foods.Price;
                    orderDetail.Quantity = orderdetail.Quantity;
                    orderDetail.Discount = (double)orderdetail.Discount;
                    orderDetail.DateBuy = (DateTime)orderdetail.CreatedAt;

                    listOrderDetail.Add(orderDetail);

                };
            }
            var listOrderTrackingAndListOrderDetail = new ListOrderTrackingAndListOrderDetail();
            listOrderTrackingAndListOrderDetail.OrderDetailsViewModels = listOrderDetail;
            listOrderTrackingAndListOrderDetail.OrderTracking = listOrderTracking;
            return listOrderTrackingAndListOrderDetail;
            
        }

        public async Task<OrderTracking> GetOrderTrackingDetails(int? id)
        {
            Debug.WriteLine("idorderiml" + id);

            return await db.OrderTrackings
               .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
