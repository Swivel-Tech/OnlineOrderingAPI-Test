using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;

namespace OnlineOrdering.Services.Interfaces
{
    public interface ICustomersService
    {
        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns></returns>
        Task<IList<CustomerDto>> GetAllCustomers();

        /// <summary>
        /// Gets the customer by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<CustomerDto?> GetCustomerById(int id);

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CustomerDto> CreateCustomer(CreateCustomerRequest request);

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="customerDto">The customer dto.</param>
        /// <returns></returns>
        Task<CustomerDto> UpdateCustomer(CustomerDto customerDto);

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task DeleteCustomer(int id);
    }
}
