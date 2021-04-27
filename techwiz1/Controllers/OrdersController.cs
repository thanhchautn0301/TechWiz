
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechWizProject.Models;
using TechWizProject.Services;

namespace TechWizProject.Controllers
{
    [Route("orders")]
    public class OrdersController : Controller
    {

        private OrderService orderService;
        private IWebHostEnvironment webHostEnvironment;

        public OrdersController(OrderService orderService, IWebHostEnvironment webHostEnvironment)
        {
            this.orderService = orderService;
            this.webHostEnvironment = webHostEnvironment;
        }


        // GET: Orders
        [Route("index")]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View(await orderService.GetAllOrder());
        }

        // GET: Orders/Details/5
        [HttpGet]
        [Route("details")]
        public IActionResult Details(int id)
        {
            return RedirectToAction("Index", "ordertrackings" +
                "", new { idOrder = id });
        }

        // GET: Orders/Create

        [Route("create")]
        public async Task<IActionResult> Create()
        {
            var listcart = new List<CartItem>();
            if (HttpContext.Session.GetString("listcart") != null)
            {
                listcart = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("listcart"));

            }

            var order = new Order();
            double total = 0;
            int foodid = 0;

            foreach (var item in listcart)
            {
                total += (item.Price * item.Quantity);
                order.UserId = item.UserId;
                foodid = item.FoodId;
            }


            order.ShipperId = orderService.GetIdRestaurant(foodid);
            order.TotalPrice = total;
            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;
            order.Status = 1;
            await orderService.Create(order, listcart);
            HttpContext.Session.Remove("listcart");
            return RedirectToAction(nameof(Index));
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("create")]
        //public async Task<IActionResult> Create([Bind("Id,UserId,TotalPrice,ShipperId,Status,CreatedAt,UpdatedAt")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        order.UserId = 2;
        //        order.ShipperId = 2;
        //        order.TotalPrice = 90000;
        //        order.CreatedAt = DateTime.Now;
        //        order.UpdatedAt = DateTime.Now;
        //        order.Status = 1;
        //        await orderService.Create(order);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(order);
        //}

        // GET: Orders/Edit/5
        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await orderService.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TotalPrice,ShipperId,Status,CreatedAt,UpdatedAt")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.UpdatedAt = DateTime.Now;
                    await orderService.Update(order);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!orderService.OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        [HttpGet]
        [Route("cancel")]
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await orderService.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                await orderService.Cancel(id);

            }

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        [Route("history")]
        public async Task<IActionResult> History()
        {




            return View("history");

        }

        // POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderExists(int id)
        //{
        //    return _context.Orders.Any(e => e.Id == id);
        //}
    }
}
