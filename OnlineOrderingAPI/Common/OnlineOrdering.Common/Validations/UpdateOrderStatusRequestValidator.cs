using FluentValidation;
using OnlineOrdering.Common.Models.Requests;

namespace OnlineOrdering.Common.Validations
{
    public class UpdateOrderStatusRequestValidator: AbstractValidator<UpdateOrderStatusRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrderStatusRequestValidator"/> class.
        /// </summary>
        public UpdateOrderStatusRequestValidator()
        {
            RuleFor(order => order.OrderId).NotEqual(0).WithMessage("Order Id cannot be zero.");
            RuleFor(order => order.OrderStatus).IsInEnum().WithMessage("Invalid order status.");
        }
    }
}
