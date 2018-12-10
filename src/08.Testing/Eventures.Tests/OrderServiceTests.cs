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
    public class OrderServiceTests
    {
        [Fact]
        public void GetMyOrdersReturnsAllOrdersOfTheCurrentlyLoggedUser()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
              .UseInMemoryDatabase(databaseName: "Get_My_Orders_Db")
              .Options;

            var db = new EventuresDbContext(options);

            db.Orders.AddRange(TestData().AsQueryable());
            db.SaveChanges();

            var currentlyLoggedInUser = db.Users.FirstOrDefault(u => u.UserName == "Pesho");

            var orderService = new OrderService(db);

            var orders = orderService.GetMyOrders(currentlyLoggedInUser.UserName);

            Assert.True(orders.Count() == 1);
        }

        [Fact]
        public void CreateOrderWorksCorrectlyAndSavesItInDb()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
             .UseInMemoryDatabase(databaseName: "Create_Order_Db")
             .Options;

            var db = new EventuresDbContext(options);

            var eventt = new Event
            {
                Id = "123",
                Name = "FootballGame",
                PricePerTicket = 10.25m,
                Place = "Somewhere",
                TotalTickets = 15000,
                Start = new DateTime(2018, 12, 23, 19, 30, 0),
                End = new DateTime(2018, 12, 23, 21, 0, 0)
            };

            var user = new EventuresUser
            {
                Id = "456",
                UserName = "Pesho",
                FirstName = "Peter",
                LastName = "Petrov",
                UCN = "1234567890",
                Email = "pesho@pesho.bg",
                PasswordHash = "21312312412412"
            };

            db.Events.Add(eventt);
            db.Users.Add(user);
            db.SaveChanges();

            var orderService = new OrderService(db);

            orderService.CreateOrder("123", "456", 5000);

            Assert.True(db.Orders.First().Event.Name == "FootballGame");
            Assert.True(db.Orders.First().Customer.UserName == "Pesho");
        }

        [Fact]
        public void AllOrdersWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
             .UseInMemoryDatabase(databaseName: "All_Orders_Db")
             .Options;

            var db = new EventuresDbContext(options);

            db.Orders.AddRange(this.TestData().AsQueryable());
            db.SaveChanges();

            Assert.True(db.Orders.Count() == 3);
            Assert.True(db.Orders.First().Event.Name == "FootballGame");
            Assert.True(db.Orders.ToList()[1].Event.Name == "PrivateParty");
            Assert.True(db.Orders.Last().Event.Name == "New Years Eve");
        }

        private List<Order> TestData()
        {
            return new List<Order>
            {
                new Order
                {
                    OrderedOn = new DateTime(2018, 12, 23, 15, 0, 0),
                    TicketsCount = 20,
                    Event = new Event
                    {
                        Name = "FootballGame",
                        PricePerTicket = 10.25m,
                        Place = "Somewhere",
                        TotalTickets = 15000,
                        Start = new DateTime(2018, 12, 23, 19, 30, 0),
                        End = new DateTime(2018, 12, 23, 21, 0, 0)
                    },
                    Customer = new EventuresUser
                    {
                        UserName = "Pesho",
                        FirstName = "Peter",
                        LastName = "Petrov",
                        UCN = "1234567890",
                        Email = "pesho@pesho.bg",
                        PasswordHash = "21312312412412"
                    }
                },
                new Order
                {
                    OrderedOn = new DateTime(2018, 9, 15, 10, 26, 35),
                    TicketsCount = 5,
                    Event = new Event
                    {
                        Name = "PrivateParty",
                        PricePerTicket = 8,
                        Place = "Lounge",
                        TotalTickets = 100,
                        Start = new DateTime(2018, 9, 20, 20, 0, 0),
                        End = new DateTime(2018, 9, 21, 4, 30, 0)
                    },
                    Customer = new EventuresUser
                    {
                        UserName = "Gosho",
                        FirstName = "Georgi",
                        LastName = "Georgiev",
                        UCN = "0123456789",
                        Email = "gosho@gosho.bg",
                        PasswordHash = "wqeqwdasfasfw"
                    }
                },
                new Order
                {
                    OrderedOn = new DateTime(2017, 10, 5, 20, 0, 0),
                    TicketsCount = 10,
                    Event = new Event
                    {
                        Name = "New Years Eve",
                        PricePerTicket = 17.30m,
                        Place = "Lounge",
                        TotalTickets = 25,
                        Start = new DateTime(2017, 12, 31, 20, 0, 0),
                        End = new DateTime(2018, 1, 1, 5, 0, 0)
                    },
                    Customer = new EventuresUser
                    {
                        UserName = "Vankata",
                        FirstName = "Ivan",
                        LastName = "Ivanov",
                        UCN = "9876543210",
                        Email = "vanko@vanko.bg",
                        PasswordHash = "hcjfgjdrthf"
                    }
                }
            };
        }
    }
}
