using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;

namespace OnlineOrdering.Services.Interfaces
{
    public interface IOrderHeadersService
    {
        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns></returns>
        Task<IList<OrderHeaderDto>> GetAllOrders();

        /// <summary>
        /// Filters the orders by status.
        /// </summary>
        /// <param name="orderStatus">The order status.</param>
        /// <returns></returns>
        Task<IList<OrderHeaderDto>> FilterOrdersByStatus(OrderStatus orderStatus);

        /// <summary>
        /// Gets the order by identifier.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        Task<OrderHeaderDto?> GetOrderById(int orderId);

        /// <summary>
        /// Gets the orders by customer identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<IList<OrderHeaderDto>> GetOrdersByCustomerId(int customerId);

        /// <summary>
        /// Creates the order header.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OrderHeaderDto> CreateOrderHeader(CreateOrderHeaderRequest request);

        /// <summary>
        /// Updates the order status.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OrderHeaderDto> UpdateOrderStatus(UpdateOrderStatusRequest request);
    }
}
