namespace OnlineOrdering.Common.Models.Requests
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}
