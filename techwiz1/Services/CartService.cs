using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.Services
{
   public interface CartService
    {
        List<CartItem> List(int userid);
        Task Create(Cart cart);
    }
}
