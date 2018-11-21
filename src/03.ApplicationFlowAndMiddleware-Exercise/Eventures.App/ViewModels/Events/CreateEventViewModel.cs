using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.App.ViewModels.Events
{
    public class CreateEventViewModel
    {
        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Display(Name = "Total Tickets")]
        public int TotalTickets { get; set; }

        [Display(Name = "Price Per Ticket")]
        public decimal PricePerTicket { get; set; }
    }
}
