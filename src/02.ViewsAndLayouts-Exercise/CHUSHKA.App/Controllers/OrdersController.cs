using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CHUSHKA.App.Controllers
{
    public class OrdersController : BaseController
    {
        public IActionResult All()
        {
            var orders = this.Db.Orders.ToList();

            return this.View(orders);
        }
    }
}