using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string FoodOrigin { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? FoodId { get; set; }
    }
}
