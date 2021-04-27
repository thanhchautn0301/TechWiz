using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
    public class CartServiceImpl : CartService
    { private readonly DatabaseContext db;
        public CartServiceImpl(DatabaseContext context)
        {
            db = context;
        }

        public async Task Create(Cart cart)
        {
            var CheckCart = db.Carts.SingleOrDefault(p => p.UserId == cart.UserId && p.FoodId == cart.FoodId);
            if (CheckCart!=null)
            {
                CheckCart.Quantity += cart.Quantity;
                db.Update(CheckCart);

            }
            else
            {
                db.Add(cart);
            }
            
            await db.SaveChangesAsync();

        }

        public List<CartItem> List(int userid)
        {
            List<CartItem> listcartitem = new List<CartItem>();
            foreach(var item in db.Carts.Where(m => m.UserId == userid).ToList())
            {
                CartItem cartItem = new CartItem();
                var food = db.Foods.SingleOrDefault(m => m.Id == item.FoodId);
                cartItem.UserId = item.UserId;
                cartItem.FoodId = item.FoodId;
                cartItem.Name = food.Name;
                cartItem.Images = food.Name;
                cartItem.Quantity = item.Quantity;
                cartItem.Price = item.Quantity * food.Price;
                listcartitem.Add(cartItem);
            }
            return listcartitem.ToList();
        }

    }
}
