using FluentValidation;
using OnlineOrdering.Common.Models.Requests;

namespace OnlineOrdering.Common.Validations
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductRequestValidator"/> class.
        /// </summary>
        public CreateProductRequestValidator()
        {
            RuleFor(cust => cust.Name).NotNull().NotEmpty().WithMessage("Customer name cannot be null or empty.");
            RuleFor(cust => cust.Price).GreaterThan(0).WithMessage("Price cannot be zero");
        }
    }
}
