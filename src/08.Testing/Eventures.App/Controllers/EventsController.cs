using Eventures.App.ViewModels.Events;
using Eventures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace Eventures.App.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventService eventService;
        private readonly OrderService orderService;

        private const int EventsPerPage = 5;

        public EventsController(EventService eventService, OrderService orderService)
        {
            this.eventService = eventService;
            this.orderService = orderService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CreateEventViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.eventService.CreateEvent(model.Name, model.Place, model.PricePerTicket, model.TotalTickets, model.Start, model.End);

            return this.RedirectToAction("All", "Events");
        }

        [Authorize]
        [HttpGet]
        public IActionResult All(int? page)
        {
            var events = this.eventService.GetAllEvents().Select(e => new AllEventsViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Start = e.Start,
                End = e.End,
                Place = e.Place
            })
            .ToList();

            var pageNumber = page ?? 1;

            var pagedEvents = events.ToPagedList(pageNumber, EventsPerPage);

            return this.View(pagedEvents);
        }

        [Authorize]
        public IActionResult MyEvents(int? page)
        {
            var events = this.orderService.GetMyOrders(this.User.Identity.Name).Select(o => new MyEventsViewModel
            {
                Name = o.Event.Name,
                Tickets = o.TicketsCount,
                Start = o.Event.Start,
                End = o.Event.End
            })
            .ToList();

            var pageNumber = page ?? 1;

            var pagedEvents = events.ToPagedList(pageNumber, EventsPerPage);

            return this.View(pagedEvents);
        }
    }
}