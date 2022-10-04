namespace OnlineOrdering.Common.Models.Requests
{
    public class CreateOrderLineItemRequest
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}
