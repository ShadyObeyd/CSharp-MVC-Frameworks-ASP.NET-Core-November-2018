using Eventures.Data;
using Eventures.Models;
using Eventures.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Eventures.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void CreateEventWorksCorrectlyAndSavesItInDb()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
                .UseInMemoryDatabase(databaseName: "Save_User_Db")
                .Options;

            var db = new EventuresDbContext(options);

            var eventService = new EventService(db);

            eventService.CreateEvent("FootballGame", "Somewhere", 10.25m, 15000, new DateTime(2018, 12, 23, 19, 30, 0), new DateTime(2018, 12, 23, 21, 0, 0));
            

            Assert.True(db.Events.Count() == 1);

            var dbEvent = db.Events.FirstOrDefault(e => e.Name == "FootballGame");

            Assert.True(dbEvent != null);
        }

        [Fact]
        public void GetAllEventsReturnsAllEventsWithAtleastOneAvailableTicket()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
               .UseInMemoryDatabase(databaseName: "Get_Events_Db")
               .Options;

            var db = new EventuresDbContext(options);

            db.Events.AddRange(this.TestEvents().AsQueryable());
            db.SaveChanges();

            EventService eventService = new EventService(db);

            var events = eventService.GetAllEvents();

            Assert.True(events.Count() == 2);
        }

        [Fact]
        public void GetEventByIdReturnsCorrectEvent()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
              .UseInMemoryDatabase(databaseName: "Get_Event_by_Id_Db")
              .Options;

            var db = new EventuresDbContext(options);

            var @event = new Event
            {
                Name = "FootballGame",
                PricePerTicket = 10.25m,
                Place = "Somewhere",
                TotalTickets = 15000,
                Start = new DateTime(2018, 12, 23, 19, 30, 0),
                End = new DateTime(2018, 12, 23, 21, 0, 0),
                Id = "123"
            };

            db.Events.AddRange(this.TestEvents().AsQueryable());
            db.Events.Add(@event);
            db.SaveChanges();

            Assert.True(db.Events.Count() == 4);

            var eventService = new EventService(db);

            var dbEvent = eventService.GetEventById("123");

            Assert.True(dbEvent.Name == "FootballGame");
        }

        [Fact]
        public void ReduceTotalTicketsWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
             .UseInMemoryDatabase(databaseName: "ReduceTickets_Db")
             .Options;

            var db = new EventuresDbContext(options);

            db.Events.AddRange(this.TestEvents().AsQueryable());
            db.SaveChanges();

            var eventService = new EventService(db);

            eventService.ReduceTotalTickets(db.Events.First(), 5000);

            Assert.True(db.Events.First().TotalTickets == 10000);
        }

        [Fact]
        public void EventIsValidReturnsCorrectResult()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
             .UseInMemoryDatabase(databaseName: "Event_Is_Valid_Db")
             .Options;

            var db = new EventuresDbContext(options);

            db.Events.AddRange(this.TestEvents().AsQueryable());
            db.SaveChanges();

            var eventService = new EventService(db);

            var eventt = db.Events.First();

            bool isValid = eventService.EventIsValid(eventt, 5000);

            Assert.True(isValid == true);

            isValid = eventService.EventIsValid(eventt, 20_000);

            Assert.True(isValid == false);
        }

        private List<Event> TestEvents()
        {
            return new List<Event>
            {
                new Event
                {
                    Name = "FootballGame",
                    PricePerTicket = 10.25m,
                    Place = "Somewhere",
                    TotalTickets = 15000,
                    Start = new DateTime(2018, 12, 23, 19, 30, 0),
                    End = new DateTime(2018, 12, 23, 21, 0, 0)
                },
                new Event
                {
                    Name = "BaseballGame",
                    PricePerTicket = 12.25m,
                    Place = "Nowhere",
                    TotalTickets = 20000,
                    Start = new DateTime(2019, 1, 15, 15, 0, 0),
                    End = new DateTime(2018, 1, 15, 16, 30, 0)
                },
                new Event
                {
                    Name = "MMA Fight",
                    PricePerTicket = 15,
                    Place = "UFC",
                    TotalTickets = 0,
                    Start = new DateTime(2018, 12, 23, 19, 30, 0),
                    End = new DateTime(2018, 12, 23, 21, 0, 0)
                }
            };
        }
    }
}
