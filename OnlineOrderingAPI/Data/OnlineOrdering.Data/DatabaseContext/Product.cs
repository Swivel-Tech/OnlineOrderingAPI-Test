using System;
using System.Collections.Generic;

namespace OnlineOrdering.Data.DatabaseContext
{
    public partial class Product
    {
        public Product()
        {
            OrderLineItems = new HashSet<OrderLineItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<OrderLineItem> OrderLineItems { get; set; }
    }
}
