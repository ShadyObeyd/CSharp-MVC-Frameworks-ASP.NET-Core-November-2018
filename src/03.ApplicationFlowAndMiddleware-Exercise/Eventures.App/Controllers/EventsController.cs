﻿using Eventures.App.Filters;
using Eventures.App.ViewModels.Events;
using Eventures.Data;
using Eventures.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Eventures.App.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventuresDbContext db;
        private readonly ILogger logger;

        public EventsController(EventuresDbContext db, ILogger<EventsController> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ServiceFilter(typeof(LogActionFilterAttribute))]
        public IActionResult Create(CreateEventViewModel model)
        {
            var @event = new Event
            {
                Name = model.Name,
                Place = model.Place,
                PricePerTicket = model.PricePerTicket,
                TotalTickets = model.TotalTickets,
                Start = model.Start,
                End = model.End
            };

            this.db.Events.Add(@event);
            this.db.SaveChanges();

            this.logger.LogInformation($"Event created: {@event.Name}", @event);

            return this.RedirectToAction("All", "Events");
        }

        [HttpGet]
        public IActionResult All()
        {
            var events = this.db.Events.Select(e => new AllEventsViewModel
            {
                Name = e.Name,
                Start = e.Start,
                End = e.End,
                Place = e.Place
            })
            .ToList();

            return this.View(events);
        }
    }
}