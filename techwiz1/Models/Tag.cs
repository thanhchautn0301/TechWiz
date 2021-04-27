using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Models
{
    public partial class Tag
    {
        public int IdTag { get; set; }
        public string NameTag { get; set; }
        public int? Status { get; set; }
    }
}
