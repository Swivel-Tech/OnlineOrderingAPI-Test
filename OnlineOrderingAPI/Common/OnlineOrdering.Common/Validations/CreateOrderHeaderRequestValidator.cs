using FluentValidation;
using OnlineOrdering.Common.Models.Requests;

namespace OnlineOrdering.Common.Validations
{
    public class CreateOrderHeaderRequestValidator: AbstractValidator<CreateOrderHeaderRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrderHeaderRequestValidator"/> class.
        /// </summary>
        public CreateOrderHeaderRequestValidator()
        {
            RuleFor(order => order.OrderLineItems).NotEmpty().NotNull().WithMessage("There should be at lease one order line item.");
            RuleFor(order => order.Customer).NotNull().NotEmpty().WithMessage("Customer cannot be empty or null");
            RuleFor(order => order.Customer.Name).NotNull().NotEmpty().WithMessage("Customer name cannot be null or empty.");
            RuleFor(order => order.Customer.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Customer email cannot be null or empty and should follow <emailid>@<domain> pattern.");
            RuleFor(order => order.Customer.Phone).NotNull().NotEmpty().Length(10).Must(x => long.TryParse(x, out long val) && val > 0).WithMessage("Customer phone number cannot be null or empty and must have 10 digits");
            RuleFor(order => order.OrderLineItems).ForEach(orderItem => orderItem.ChildRules(d => d.RuleFor(d => d.ProductId).GreaterThan(0))).WithMessage("Product ids of order line items cannot be zero.");
            RuleFor(order => order.Status).IsInEnum().WithMessage("Invalid order status.");
        }
    }
}
