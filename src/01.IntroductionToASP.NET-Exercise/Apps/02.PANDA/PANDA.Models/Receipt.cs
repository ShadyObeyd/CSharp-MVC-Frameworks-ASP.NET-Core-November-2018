using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PANDA.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public int MyProperty { get; set; }

        public virtual User Recipient { get; set; }

        public int RecipientId { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }

        public int PackageId { get; set; }
    }
}
