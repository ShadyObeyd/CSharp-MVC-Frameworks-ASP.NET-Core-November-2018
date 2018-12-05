using System;

namespace Eventures.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public Event Event { get; set; }

        public string EventId { get; set; }

        public EventuresUser Customer { get; set; }

        public string CustomerId { get; set; }

        public DateTime OrderedOn { get; set; }

        public int TicketsCount { get; set; }
    }
}
