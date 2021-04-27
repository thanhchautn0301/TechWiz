using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Areas.Admin.Models
{
    public partial class Cart
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
