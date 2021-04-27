using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Areas.Admin.Models
{
    public partial class Restaurant
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Addr { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string Cover { get; set; }
        public string Logo { get; set; }
        public double? ShippingFeePerKm { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? IdTag { get; set; }
    }
}
