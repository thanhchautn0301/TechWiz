using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechWizProject.Areas.Admin.Services;
using TechWizProject.Areas.Admin.Models;

namespace TechWizProject.Controllers
{
    [Area("Admin")]
    [Route("Admin/Restaurants")]
    public class RestaurantsController : Controller
    {

        private IRestaurantsServies restaurantsServies;
        public RestaurantsController(IRestaurantsServies _restaurantsServies)
        {

            restaurantsServies = _restaurantsServies;
        }

        //GET: Restaurants
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.listRes = await restaurantsServies.FindAll();
            return View();
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await restaurantsServies.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        [Route("create")]
        // GET: Restaurants/Create
        public IActionResult Create()
        {
            var restaurants = new Restaurant();
            return View(restaurants);
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerId,Name,Addr,Lat,Lng,Cover,Logo,ShippingFeePerKm,Status,CreatedAt,UpdatedAt")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                await restaurantsServies.Create(restaurant);
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5

        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await restaurantsServies.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,Name,Addr,Lat,Lng,Cover,Logo,ShippingFeePerKm,Status,CreatedAt,UpdatedAt")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await restaurantsServies.Edit(restaurant);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.Id))
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
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        [Route("Delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Restaurant restaurant = await restaurantsServies.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await restaurantsServies.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return restaurantsServies.ResExists(id);
        }
    }
}
