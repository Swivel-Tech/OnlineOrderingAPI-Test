using System;
using System.Collections.Generic;

namespace OnlineOrdering.Data.DatabaseContext
{
    public partial class OrderHeader
    {
        public OrderHeader()
        {
            OrderLineItems = new HashSet<OrderLineItem>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderLineItem> OrderLineItems { get; set; }
    }
}
