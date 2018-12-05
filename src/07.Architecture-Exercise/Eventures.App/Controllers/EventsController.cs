﻿using Eventures.App.ViewModels.Events;
using Eventures.Data;
using Eventures.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace Eventures.App.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventuresDbContext db;
        private readonly ILogger logger;

        private const int EventsPerPage = 5;

        public EventsController(EventuresDbContext db, ILogger<EventsController> logger)
        {
            this.db = db;
            this.logger = logger;
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

            return this.RedirectToAction("All", "Events");
        }

        [Authorize]
        [HttpGet]
        public IActionResult All(int? page)
        {
            var events = this.db.Events.Where(e => e.TotalTickets > 0).Select(e => new AllEventsViewModel
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
            var events = this.db.Orders.Where(o => o.Customer.UserName == this.User.Identity.Name).Select(o => new MyEventsViewModel
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