using Eventures.App.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.App.ViewModels.Events
{
    public class CreateEventViewModel
    {
        [Required]
        [MinLength(10, ErrorMessage = Constants.EventNameLengthErrorMessage)]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        [Required(ErrorMessage = Constants.EventStartErrorMessage)]
        [DataType(DataType.DateTime, ErrorMessage = Constants.EventStartErrorMessage)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = Constants.EventEndErrorMessage)]
        [DataType(DataType.DateTime, ErrorMessage = Constants.EventEndErrorMessage)]
        public DateTime End { get; set; }
        
        [Required]
        [Display(Name = "Total Tickets")]
        [Range(0, int.MaxValue)]
        public int TotalTickets { get; set; }

        [Required]
        [Display(Name = "Price Per Ticket")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal PricePerTicket { get; set; }
    }
}
