using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Models;

namespace TechWizProject.ViewModels
{
    public class ListOrderTrackingAndListOrderDetail
    {
        public List<OrderDetailsViewModel> OrderDetailsViewModels { get; set; }
        public List<OrderTracking> OrderTracking { get; set; }

    }
}
