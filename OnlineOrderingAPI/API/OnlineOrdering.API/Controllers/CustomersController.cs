using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;
using OnlineOrdering.Services.Interfaces;

namespace OnlineOrdering.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customersService;

        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<CustomerDto>), 200)]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await customersService.GetAllCustomers());
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            return Ok(await customersService.GetCustomerById(customerId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        public async Task<IActionResult> CreateCustomer([FromBody]CreateCustomerRequest request)
        {
            return Ok(await customersService.CreateCustomer(request));    
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        public async Task<IActionResult> UpdateCustomer([FromBody]CustomerDto request)
        {
            return Ok(await customersService.UpdateCustomer(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomerById([FromQuery]int customerId)
        {
            await customersService.DeleteCustomer(customerId);
            return Ok();
        }
    }
}
