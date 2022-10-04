using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Data.Interfaces;
using OnlineOrdering.Services.Interfaces;
using OnlineOrdering.Common.Utils;
using OnlineOrdering.Data.DatabaseContext;
using OnlineOrdering.Common.Models.Requests;
using OnlineOrdering.Common.Exceptions;

namespace OnlineOrdering.Services
{
    public class CustomersService: BaseService, ICustomersService
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        public CustomersService(IUnitOfWork unitOfWork, IMapper mapper): base(mapper) 
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CustomerDto>> GetAllCustomers()
        {
            var customers = await unitOfWork.Customers.GetAll().ToListAsync();
            return AutoMapperUtility<IList<Customer>, IList<CustomerDto>>.GetMappedObject(customers, mapper);
        }

        /// <summary>
        /// Gets the customer by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<CustomerDto?> GetCustomerById(int id)
        {
            var customer = await unitOfWork.Customers.FindByCondition(c => c.Id == id).FirstOrDefaultAsync();

            if (customer == null)
                throw new CustomException("Customer not found.", System.Net.HttpStatusCode.BadRequest);

            return AutoMapperUtility<Customer?, CustomerDto?>.GetMappedObject(customer, mapper);

        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<CustomerDto> CreateCustomer(CreateCustomerRequest request)
        {
            var existing = await unitOfWork.Customers.FindByCondition(cust => cust.Email == request.Email || cust.Phone == request.Phone).SingleOrDefaultAsync();

            if (existing != null)
                throw new CustomException("Customer email or phone number is already in use.", System.Net.HttpStatusCode.BadRequest);

            var customer = AutoMapperUtility<CreateCustomerRequest, Customer>.GetMappedObject(request, mapper);

            await unitOfWork.Customers.CreateAsync(customer);
            await unitOfWork.Complete();
            
            return AutoMapperUtility<Customer, CustomerDto>.GetMappedObject(customer, mapper);
        }

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="customerDto">The customer dto.</param>
        /// <returns></returns>
        public async Task<CustomerDto> UpdateCustomer(CustomerDto customerDto)
        {
            var existing = await unitOfWork.Customers.FindByCondition(cust => cust.Id == customerDto.Id).SingleOrDefaultAsync();

            if (existing == null)
                throw new CustomException("Customer not found.", System.Net.HttpStatusCode.BadRequest);

            existing.Email = customerDto.Email;
            existing.Phone = customerDto.Phone; 
            existing.Name = customerDto.Name;
            existing.IsActive = customerDto.IsActive;

            unitOfWork.Customers.Update(existing);
            await unitOfWork.Complete();
            return AutoMapperUtility<Customer, CustomerDto>.GetMappedObject(existing, mapper);
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="OnlineOrdering.Common.Exceptions.CustomException">Product not found.</exception>
        public async Task DeleteCustomer(int id)
        {
            var customer = await unitOfWork.Customers.FindByCondition(p => p.Id == id).FirstOrDefaultAsync();

            if (customer == null)
                throw new CustomException("Customer not found.", System.Net.HttpStatusCode.BadRequest);

            customer.IsActive = false;

            unitOfWork.Customers.Update(customer);
            await unitOfWork.Complete();
                
        }
    }
}
