using OnlineOrdering.Common.Enums;

namespace OnlineOrdering.Common.Models.Requests
{
    public class UpdateOrderStatusRequest
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
