using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechWizProject.Models;
using TechWizProject.Services;

namespace TechWizProject.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly CartService cartService;

        public CartController(CartService _cartService, DatabaseContext context)
        {
            cartService = _cartService;
            _context = context;
        }

        // GET: Cart
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            int userid = 1;
            ViewBag.cartItems = cartService.List(userid);
            return View();
        }

        // GET: Cart/Create
        [Route("create")]
        public IActionResult Create()
        {
            return View("create");
        }

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FoodId,Quantity,Status,CreatedAt,UpdatedAt")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                await cartService.Create(cart);
                return RedirectToAction("index");
            }
            return View(cart);
        }

        // GET: Cart/Edit/5
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(CartItem cartItem)
        {
            var cart = _context.Carts.SingleOrDefault(m => m.UserId == cartItem.UserId && m.FoodId == cartItem.FoodId);
            cart.Quantity = cartItem.Quantity;
            _context.Update(cart);
            await _context.SaveChangesAsync();
            ViewBag.cartItems = cartService.List(cartItem.UserId);
            return RedirectToAction("index");
        }




        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("UserId,FoodId,Quantity,Status,CreatedAt,UpdatedAt")] Cart cart)
        //{
        //    if (id != cart.UserId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(cart);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CartExists(cart.UserId))
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
        //    return View(cart);
        //}

        // GET: Cart/Delete/5
        [Route("delete/{userid}/{foodid}")]
        public async Task<IActionResult> Delete(int userid, int foodid)
        {
            try
            {
                //int quan = Int32.Parse( Request.Form[id]);

                var cart = _context.Carts.SingleOrDefault(m => m.UserId == userid && m.FoodId == foodid);
                cart.Quantity = 66;

                _context.Remove(cart);
                await _context.SaveChangesAsync();
                ViewBag.cartItems = cartService.List(userid);
                return RedirectToAction("index");

            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        //// POST: Cart/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var cart = await _context.Carts.FindAsync(id);
        //    _context.Carts.Remove(cart);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.UserId == id);
        }

        [Route("checkout")]
        public IActionResult CheckOut()
        {
            int iduser = 1;
            var ListCart = cartService.List(iduser);
            HttpContext.Session.SetString("listcart", JsonConvert.SerializeObject(ListCart));

            return RedirectToAction("create", "orders");

        }
    }
}
