using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Models
{
    public partial class City
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
