using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Areas.Admin.Models
{
    public partial class FoodRating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public double? Point { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
