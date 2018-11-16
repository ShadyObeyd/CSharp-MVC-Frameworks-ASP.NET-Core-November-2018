using PANDA.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PANDA.Models
{
    public class Package
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public Status Status { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public virtual User Recipient { get; set; }

        public int RecipientId { get; set; }

        [ForeignKey("ReceiptId")]
        public virtual Receipt Receipt { get; set; }

        public int ReceiptId { get; set; }
    }
}
