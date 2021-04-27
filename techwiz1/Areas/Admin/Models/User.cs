using System;
using System.Collections.Generic;

#nullable disable

namespace TechWizProject.Areas.Admin.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FbId { get; set; }
        public string GgId { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
