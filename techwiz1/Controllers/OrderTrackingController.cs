using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechWizProject.Models;
using TechWizProject.Services;

namespace TechWizProject.Controllers
{
    [Route("ordertrackings")]
    public class OrderTrackingsController : Controller
    {
        private OrderTrackingService orderTrackingService;
        private IWebHostEnvironment webHostEnvironment;

        public OrderTrackingsController(OrderTrackingService _orderTrackingService, IWebHostEnvironment webHostEnvironment)
        {
            this.orderTrackingService = _orderTrackingService;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: OrderTrackings
        [Route("index")]
        [Route("")]
        public async Task<IActionResult> Index(int idOrder)
        {

            return View(await orderTrackingService.GetAllOrderTrackingAndAllElementInOrderDetailsById(idOrder));
        }

        // GET: OrderTrackings/Details/5
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Debug.WriteLine("idorder" + id);
            var orderTracking = await orderTrackingService.GetOrderTrackingDetails(id);
            if (orderTracking == null)
            {
                return NotFound();
            }

            return View(orderTracking);
        }


        [HttpGet]
        [Route("accepted")]
        public async Task<IActionResult> Accepted(int id)
        {

            await orderTrackingService.AcceptOrder(id);


            return RedirectToAction("index", "orders");

        }

        //// GET: OrderTrackings/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: OrderTrackings/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,OrderId,State,Status,CreatedAt,UpdatedAt")] OrderTracking orderTracking)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(orderTracking);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(orderTracking);
        //}

        //// GET: OrderTrackings/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderTracking = await _context.OrderTrackings.FindAsync(id);
        //    if (orderTracking == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(orderTracking);
        //}

        //// POST: OrderTrackings/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,State,Status,CreatedAt,UpdatedAt")] OrderTracking orderTracking)
        //{
        //    if (id != orderTracking.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(orderTracking);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderTrackingExists(orderTracking.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(orderTracking);
        //}

        //// GET: OrderTrackings/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderTracking = await _context.OrderTrackings
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (orderTracking == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(orderTracking);
        //}

        //// POST: OrderTrackings/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var orderTracking = await _context.OrderTrackings.FindAsync(id);
        //    _context.OrderTrackings.Remove(orderTracking);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderTrackingExists(int id)
        //{
        //    return _context.OrderTrackings.Any(e => e.Id == id);
        //}
    }
}
