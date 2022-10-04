using System;
using System.Collections.Generic;

namespace OnlineOrdering.Data.DatabaseContext
{
    public partial class Customer
    {
        public Customer()
        {
            OrderHeaders = new HashSet<OrderHeader>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
