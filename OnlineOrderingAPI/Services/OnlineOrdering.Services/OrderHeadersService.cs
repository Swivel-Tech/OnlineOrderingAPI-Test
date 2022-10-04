using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Exceptions;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;
using OnlineOrdering.Common.Utils;
using OnlineOrdering.Data.DatabaseContext;
using OnlineOrdering.Data.Interfaces;
using OnlineOrdering.Services.Interfaces;

namespace OnlineOrdering.Services
{
    public class OrderHeadersService: BaseService, IOrderHeadersService
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of OrderHeadersService
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public OrderHeadersService(IUnitOfWork unitOfWork, IMapper mapper): base(mapper)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<OrderHeaderDto>> GetAllOrders()
        {
            var orders = await unitOfWork.Orders.GetAll().Include(o => o.OrderLineItems).ToListAsync();
            return AutoMapperUtility<IList<OrderHeader>, IList<OrderHeaderDto>>.GetMappedObject(orders, mapper); ;
        }

        /// <summary>
        /// Filters the orders by status.
        /// </summary>
        /// <param name="orderStatus">The order status.</param>
        /// <returns></returns>
        public async Task<IList<OrderHeaderDto>> FilterOrdersByStatus(OrderStatus orderStatus)
        {
            var orders = await unitOfWork.Orders.FindByCondition(o => o.Status == orderStatus.ToString())
                .Include(o => o.OrderLineItems)
                .Include(o => o.Customer)
                .ToListAsync();
            return AutoMapperUtility<IList<OrderHeader>, IList<OrderHeaderDto>>.GetMappedObject(orders, mapper); ;
        }

        /// <summary>
        /// Gets the order by identifier.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        /// <exception cref="OnlineOrdering.Common.Exceptions.CustomException">Customer not found.</exception>
        public async Task<OrderHeaderDto?> GetOrderById(int orderId)
        {
            var order = await unitOfWork.Orders.FindByCondition(o => o.Id == orderId)
                .Include(o => o.OrderLineItems)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new CustomException("Customer not found.", System.Net.HttpStatusCode.BadRequest);
                

            return AutoMapperUtility<OrderHeader?, OrderHeaderDto?>.GetMappedObject(order, mapper);
        }

        /// <summary>
        /// Gets the orders by customer identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        /// <exception cref="OnlineOrdering.Common.Exceptions.CustomException">Customer not found.</exception>
        public async Task<IList<OrderHeaderDto>> GetOrdersByCustomerId(int customerId)
        {
            var orders = await unitOfWork.Orders.FindByCondition(o => o.CustomerId == customerId)
                .Include(o => o.OrderLineItems)
                .Include(o => o.Customer)
                .ToListAsync();

            if (orders == null)
                throw new CustomException("Customer not found.", System.Net.HttpStatusCode.BadRequest);

            return AutoMapperUtility<IList<OrderHeader>, IList<OrderHeaderDto>>.GetMappedObject(orders, mapper);
        }

        /// <summary>
        /// Creates the order header.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OrderHeaderDto> CreateOrderHeader(CreateOrderHeaderRequest request)
        {
            var orderHeader = AutoMapperUtility<CreateOrderHeaderRequest, OrderHeader>.GetMappedObject(request, mapper);

            if (request.Customer.Id != 0)
            {
                var customer = await unitOfWork.Customers.FindByCondition(c => c.Email.Equals(orderHeader.Customer.Email)).FirstOrDefaultAsync();
                
                if (customer == null)
                    throw new CustomException("Customer not found.", System.Net.HttpStatusCode.BadRequest);

                if (!customer.IsActive.Value)
                    throw new CustomException("Customer has been deactivated.", System.Net.HttpStatusCode.BadRequest);

                orderHeader.Customer = customer;
            }

            (orderHeader.OrderLineItems, orderHeader.TotalPrice) = await MapAndValidateOrderLineItems(request.OrderLineItems);

            await unitOfWork.Orders.CreateAsync(orderHeader);
            await unitOfWork.Complete();
            return AutoMapperUtility<OrderHeader, OrderHeaderDto>.GetMappedObject(orderHeader, mapper);
        }

        /// <summary>
        /// Updates the order status.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="OnlineOrdering.Common.Exceptions.CustomException">Order not found</exception>
        public async Task<OrderHeaderDto> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            var order = await unitOfWork.Orders.FindByCondition(o => o.Id == request.OrderId).FirstOrDefaultAsync();

            if (order == null)
                throw new CustomException("Order not found", System.Net.HttpStatusCode.BadRequest);

            order.Status = request.OrderStatus.ToString();

            unitOfWork.Orders.Update(order);
            await unitOfWork.Complete();
            return AutoMapperUtility<OrderHeader, OrderHeaderDto>.GetMappedObject(order, mapper);
        }

        #region Helper methods

        /// <summary>
        /// Mapping and Validating Order Line Items
        /// </summary>
        /// <param name="lineItems"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private async Task<(IList<OrderLineItem>, decimal totalAmount)> MapAndValidateOrderLineItems(ICollection<CreateOrderLineItemRequest> lineItems)
        {
            var orderLineItems = new List<OrderLineItem>();
            decimal totalAmount = 0;

            foreach (var item in lineItems)
            {
                var product = await unitOfWork.Products.FindByCondition(p => p.Id == item.ProductId).SingleOrDefaultAsync();

                if (product == null)
                    throw new CustomException("Product validation failed.", System.Net.HttpStatusCode.BadRequest);

                if (!product.IsActive.Value)
                    throw new CustomException("This order contains deactivated products.");

                orderLineItems.Add(new OrderLineItem()
                {
                    ProductId = product.Id,
                    LineAmount = item.Qty * product.Price,
                    Qty = item.Qty
                });

                totalAmount += item.Qty * product.Price;
            }

            return (orderLineItems, totalAmount);
        }

        #endregion Helper methods
    }
}
