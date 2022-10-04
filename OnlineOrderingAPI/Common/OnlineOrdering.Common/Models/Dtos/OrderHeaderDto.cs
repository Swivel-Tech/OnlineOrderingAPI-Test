namespace OnlineOrdering.Common.Models.Dtos
{
    public class OrderHeaderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = null!;
        public CustomerDto Customer { get; set; } = null!;
        public ICollection<OrderLineItemDto> OrderLineItems { get; set; } = null!;
    }
}
