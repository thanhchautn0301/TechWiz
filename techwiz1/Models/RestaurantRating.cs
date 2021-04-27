using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Models
{
    public partial class RestaurantRating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public double Point { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
