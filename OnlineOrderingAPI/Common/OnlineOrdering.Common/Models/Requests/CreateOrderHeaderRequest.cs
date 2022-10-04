using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace OnlineOrdering.Common.Models.Requests
{
    public class CreateOrderHeaderRequest
    {
        public CustomerDto Customer { get; set; } = null!;
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus Status { get; set; }
        public ICollection<CreateOrderLineItemRequest> OrderLineItems { get; set; } = null!;
    }
}
