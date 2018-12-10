using System.Linq;
using Eventures.App.ViewModels.Orders;
using Eventures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.App.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService orderService;
        private readonly EventService eventService;
        private readonly UserService userService;

        public OrdersController(OrderService orderService, EventService eventService, UserService userService)
        {
            this.orderService = orderService;
            this.eventService = eventService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Create(string eventId, int ticketsCount)
        {
            var @event = this.eventService.GetEventById(eventId);

            this.eventService.ReduceTotalTickets(@event, ticketsCount);

            if (!this.eventService.EventIsValid(@event, ticketsCount))
            {
                var model = new OrderErrorViewModel
                {
                    AvailableTickets =  @event.TotalTickets
                };

                return this.View("Error", model);
            }

            var user = this.userService.GetUserByUsername(this.User.Identity.Name);

            this.orderService.CreateOrder(eventId, user.Id, ticketsCount);

            return this.RedirectToAction("MyEvents", "Events");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            var orders = this.orderService.GetAllOrders().Select(o => new AllOrdersViewModel
            {
                CustomerName = o.Customer.UserName,
                EventName = o.Event.Name,
                OrderedOn = o.OrderedOn
            })
            .ToList();

            return this.View(orders);
        }
    }
}