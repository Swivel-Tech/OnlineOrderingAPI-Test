namespace OnlineOrdering.Common.Models.Dtos
{
    public class OrderLineItemDto
    {
        public long Id { get; set; }
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal LineAmount { get; set; }
    }
}
