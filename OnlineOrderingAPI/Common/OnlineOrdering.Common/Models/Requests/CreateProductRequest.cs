namespace OnlineOrdering.Common.Models.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
