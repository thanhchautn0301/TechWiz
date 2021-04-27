using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Areas.Users.Services;

namespace TechWizProject.Areas.Users.Controllers
{
    [Area("staff")]
    [Route("order")]
    public class OrderController : Controller
    {
        private IOrderService orderService;
        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        [Route("")]
        public IActionResult Index()
        {
            var loarOrder = orderService.LoadOrder();
            ViewBag.orders = loarOrder;
            return View("Index");
        }
        [Route("search")]
        public IActionResult Search([FromQuery(Name = "orderId")] int orderId) {
            var searchOrder = orderService.SearchOrder(orderId);
            ViewBag.orders = searchOrder;
            return View("Index");
        }
    }
}
