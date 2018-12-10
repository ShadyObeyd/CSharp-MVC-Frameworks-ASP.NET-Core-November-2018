using Eventures.Data;
using Eventures.Models;
using System;
using System.Linq;

namespace Eventures.Services
{
    public class EventService
    {
        private readonly EventuresDbContext db;

        public EventService(EventuresDbContext db)
        {
            this.db = db;
        }

        public void CreateEvent(string name, string place, decimal pricePerTicket, int totalTickets, DateTime start, DateTime end)
        {
            var @event = new Event
            {
                Name = name,
                Place = place,
                PricePerTicket = pricePerTicket,
                TotalTickets = totalTickets,
                Start = start,
                End = end
            };

            this.db.Events.Add(@event);
            this.db.SaveChanges();
        }

        public IQueryable<Event> GetAllEvents()
        {
            return db.Events.Where(e => e.TotalTickets > 0);
        }

        public Event GetEventById(string eventId)
        {
            return this.db.Events.FirstOrDefault(e => e.Id == eventId);
        }

        public void ReduceTotalTickets(Event @event, int ticketsCount)
        {
            @event.TotalTickets -= ticketsCount;

            this.db.Events.Update(@event);
            this.db.SaveChanges();
        }

        public bool EventIsValid(Event @event, int ticketsCount)
        {
            return @event.TotalTickets >= ticketsCount;
        }
    }
}
