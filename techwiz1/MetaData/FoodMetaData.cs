using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechWizProject.MetaData
{
    public class FoodMetaData
    {
        public int Id { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]

        public int? CategoryId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public double Price { get; set; }
        [Required]

        public string Images { get; set; }
        [Required]

        public int Status { get; set; }
        [Required]

        public DateTime? CreatedAt { get; set; }
        [Required]

        public DateTime? UpdatedAt { get; set; }
    }

    [ModelMetadataType(typeof(FoodMetaData))]
    public partial class Food
    {

    }
}
