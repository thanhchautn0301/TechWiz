using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechWizProject.Models
{
    public partial class CartItem
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Images { get; set; }
    }
}
