using Eventures.Data;
using Eventures.Models;
using System;
using System.Linq;

namespace Eventures.Services
{
    public class OrderService
    {
        private readonly EventuresDbContext db;

        public OrderService(EventuresDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Order> GetMyOrders(string username)
        {
            return db.Orders.Where(o => o.Customer.UserName == username);
        }

        public void CreateOrder(string eventId, string userId, int ticketsCount)
        {
            var order = new Order
            {
                EventId = eventId,
                CustomerId = userId,
                OrderedOn = DateTime.Now,
                TicketsCount = ticketsCount
            };

            this.db.Orders.Add(order);
            this.db.SaveChanges();
        }

        public IQueryable<Order> GetAllOrders()
        {
            return this.db.Orders;
        }
    }
}
