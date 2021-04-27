using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Models
{
    public partial class Food
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Images { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
