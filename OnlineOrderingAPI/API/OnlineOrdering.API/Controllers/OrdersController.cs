using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Exceptions;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;
using OnlineOrdering.Services.Interfaces;

namespace OnlineOrdering.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderHeadersService orderHeadersService;

        public OrdersController(IOrderHeadersService orderHeadersService)
        {
            this.orderHeadersService = orderHeadersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<OrderHeaderDto>), 200)]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await orderHeadersService.GetAllOrders());
        }

        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(typeof(IList<OrderHeaderDto>), 200)]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
        {
            return Ok(await orderHeadersService.GetOrdersByCustomerId(customerId));
        }

        [HttpGet("filter-by")]
        [ProducesResponseType(typeof(IList<OrderHeaderDto>), 200)]
        public async Task<IActionResult> GetPendingOrders([FromQuery]string orderStatus)
        {
            var result = Enum.TryParse(typeof(OrderStatus), orderStatus, out var status);
            if (result == false)
            {
                throw new CustomException("Invalid order status", System.Net.HttpStatusCode.BadRequest);
            }

            return Ok(await orderHeadersService.FilterOrdersByStatus((OrderStatus)Enum.Parse(typeof(OrderStatus), status.ToString())));
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderHeaderDto), 200)]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            return Ok(await orderHeadersService.GetOrderById(orderId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderHeaderDto), 200)]
        public async Task<IActionResult> PlaceOrder([FromBody]CreateOrderHeaderRequest request)
        {
            return Ok(await orderHeadersService.CreateOrderHeader(request));
        }

        [HttpPatch]
        [ProducesResponseType(typeof(OrderHeaderDto), 200)]
        public async Task<IActionResult> UpdateOrderStatus([FromBody]UpdateOrderStatusRequest request)
        {
            return Ok(await orderHeadersService.UpdateOrderStatus(request));
        }
    }
}
