using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
