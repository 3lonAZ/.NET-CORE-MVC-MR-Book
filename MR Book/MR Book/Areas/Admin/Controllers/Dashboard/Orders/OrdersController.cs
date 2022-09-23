using Microsoft.AspNetCore.Mvc;
using MR_Book.Areas.Admin.Models;
using MR_Book.Areas.Admin.Models.Crud_Operations;
using MR_Book.Models.Filter;
using System.Linq;

namespace MR_Book.Areas.Admin.Controllers.Dashboard.Orders
{
    [AdminFilter]

    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly ICrudSpecial<OrderModel> _orderModel;
        public OrdersController(ICrudSpecial<OrderModel> orderModel)
        {
            _orderModel = orderModel;
        }
        [Route("Admin/Dashboard/Orders")]
        public IActionResult Index()
        {
            var orders = _orderModel.Read();
            return View(orders);
        }

        [Route("~/Admin/Dashboard/Orders/Delete/{id}")]
        [HttpGet]
        public IActionResult DeleteById(int id)
        {
            _orderModel.Remove(id);
            return RedirectToAction("Index");
        }
        [Route("~/Admin/Dashboard/Orders/Delete")]
        [HttpPost]
        public IActionResult DeleteOrders()
        {
            _orderModel.Delete();
            return RedirectToAction("Index");
        }
    }
}
