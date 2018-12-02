using System;
using System.Linq;
using Eventures.App.ViewModels.Orders;
using Eventures.Data;
using Eventures.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.App.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EventuresDbContext db;

        public OrdersController(EventuresDbContext db)
        {
            this.db = db;
        }

        [Authorize]
        public IActionResult Create(string eventId, int ticketsCount)
        {
            var @event = this.db.Events.FirstOrDefault(e => e.Id == eventId);

            @event.TotalTickets -= ticketsCount;

            this.db.Events.Update(@event);
            this.db.SaveChanges();

            if (@event.TotalTickets < ticketsCount)
            {
                var model = new OrderErrorViewModel
                {
                    AvailableTickets =  @event.TotalTickets
                };

                return this.View("Error", model);
            }

            var user = this.db.Users.FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            var order = new Order
            {
                EventId = eventId,
                CustomerId = user.Id,
                OrderedOn = DateTime.Now,
                TicketsCount = ticketsCount
            };

            this.db.Orders.Add(order);
            this.db.SaveChanges();

            return this.RedirectToAction("MyEvents", "Events");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            var orders = this.db.Orders.Select(o => new AllOrdersViewModel
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