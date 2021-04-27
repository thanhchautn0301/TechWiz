using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Areas.Admin.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double TotalPrice { get; set; }
        public int? ShipperId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
