using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Models
{
    public partial class OrderTracking
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string State { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
