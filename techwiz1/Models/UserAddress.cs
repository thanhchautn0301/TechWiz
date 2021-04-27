using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Models
{
    public partial class UserAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Addr { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
