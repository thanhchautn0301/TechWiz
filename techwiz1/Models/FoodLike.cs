using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Models
{
    public partial class FoodLike
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
