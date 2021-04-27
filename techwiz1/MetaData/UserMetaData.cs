using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;
namespace TechWizProject.Model
{
    public class UserMetaData
    {
        [Required(ErrorMessage = "Please enter email !!")]
        [RegularExpression("^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Please enter the correct format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password !!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter lastname !!")]
        
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter firstname !!")]
        
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter phone number !!")]
        
        public string Phone { get; set; }
    }
    [ModelMetadataType(typeof(UserMetaData))]
    public partial class User
    {

    }
}
