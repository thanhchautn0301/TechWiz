using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public class OrderServiceImpl : OrderService
    {
        private DatabaseContext db;

        public OrderServiceImpl(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task Create(Order order, List<CartItem> listcart)
        {
            var ord = AddOrder(order);

            AddOrderDetails(order.Id, listcart);
            AddOrderTrackings(ord);
            var queryaccounts =
            from ordertracking in db.OrderTrackings
            where
            ordertracking.OrderId == ord.Id && ordertracking.State != "Process"
            select ordertracking;
            foreach (var ordertracking in queryaccounts)
            {
                ordertracking.Status = 0;
                Debug.WriteLine("state " + ordertracking.State);
                Debug.WriteLine("status " + ordertracking.Status);

            }
            DeleteAllCart();
            await db.SaveChangesAsync();

        }
        private Order AddOrder(Order order)
        {
            db.Add(order);
            db.SaveChanges();
            return order;

        }
        private async void AddOrderDetails(int idorder, List<CartItem> listcart)
        {
            foreach (var item in listcart)
            {
                await db.OrderDetails.AddAsync(
                 new OrderDetail { OrderId = idorder, FoodId = item.FoodId, FoodOrigin = item.Name, Price = item.Price, Quantity = item.Quantity, Discount = 0, Status = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }

                );
            }

            //  await db.SaveChangesAsync();
        }

        private async void AddOrderTrackings(Order order)
        {

            await db.OrderTrackings.AddRangeAsync(
              new OrderTracking { OrderId = order.Id, State = "Process", Status = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
              new OrderTracking { OrderId = order.Id, State = "Accepted", Status = 0, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
              new OrderTracking { OrderId = order.Id, State = "Prepared", Status = 0, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
              new OrderTracking { OrderId = order.Id, State = "Wait for Shipper", Status = 0, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
              new OrderTracking { OrderId = order.Id, State = "On The Way", Status = 0, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
              new OrderTracking { OrderId = order.Id, State = "Delivered", Status = 0, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
              new OrderTracking { OrderId = order.Id, State = "Cancel", Status = 0, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
              );




        }

        public async Task Cancel(int? id)
        {
            var order = (from x in db.Orders
                         where x.Id == id
                         select x).First();
            order.UpdatedAt = DateTime.Now;
            order.Status = 0;

            var orderTracking = (from x in db.OrderTrackings
                                 where x.OrderId == id && x.State == "Cancel"
                                 select x).First();
            orderTracking.Status = 1;

            await db.SaveChangesAsync();
        }

        public async Task<Order> Find(int? id)
        {
            return await db.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetAllOrder()
        {
            return await db.Orders.Where(p => p.Status == 1).ToListAsync();
        }

        //public async Task<Order> GetOrderDetail(int? id)
        //{
        //   return await db.Orders
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //}

        public bool OrderExists(int id)
        {
            return db.Orders.Any(e => e.Id == id);

        }

        public async Task Update(Order order)
        {
            db.Update(order);
            await db.SaveChangesAsync();
        }

        public int GetIdRestaurant(int foodid)
        {
            return db.Foods.Where(p => p.Id == foodid).Select(x => x.RestaurantId).FirstOrDefault();

        }

        public void DeleteAllCart()
        {
            foreach (var entity in db.Carts)
                db.Carts.Remove(entity);
        }
    }
}
