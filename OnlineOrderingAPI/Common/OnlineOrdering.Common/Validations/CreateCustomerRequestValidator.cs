using FluentValidation;
using OnlineOrdering.Common.Models.Requests;

namespace OnlineOrdering.Common.Validations
{
    public class CreateCustomerRequestValidator: AbstractValidator<CreateCustomerRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomerRequestValidator"/> class.
        /// </summary>
        public CreateCustomerRequestValidator()
        {
            RuleFor(cust => cust.Name).NotNull().NotEmpty().WithMessage("Customer name cannot be null or empty.");
            RuleFor(cust => cust.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Customer email cannot be null or empty and should follow <emailid>@<domain> pattern.");
            RuleFor(cust => cust.Phone).NotNull().NotEmpty().Length(10).Must(x => long.TryParse(x, out long val) && val > 0).WithMessage("Customer phone number cannot be null or empty and must have 10 digits");
        }
    }
}
