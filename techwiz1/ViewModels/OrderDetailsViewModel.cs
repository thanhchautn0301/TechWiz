using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.ViewModels
{
    public class OrderDetailsViewModel
    {
        public string Image { get; set; }
        public string FoodName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public DateTime DateBuy { get; set; }
    }
}
