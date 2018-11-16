namespace CHUSKA.Models
{
    using System;

    public class Order
    {
        public int Id { get; set; }

        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        public virtual ChushkaUser Client { get; set; }

        public string ClientId { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
