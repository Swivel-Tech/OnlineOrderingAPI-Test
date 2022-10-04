using System;
using System.Collections.Generic;

namespace OnlineOrdering.Data.DatabaseContext
{
    public partial class OrderLineItem
    {
        public long Id { get; set; }
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal LineAmount { get; set; }

        public virtual OrderHeader OrderHeader { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
