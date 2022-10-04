using Moq;
using OnlineOrdering.Common.Exceptions;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;
using OnlineOrdering.Services.Interfaces;

namespace OnlineOrdering.API.Tests.Mocks
{
    public class MockICustomersService
    {
        public static Mock<ICustomersService> GetMock()
        {
            var mock = new Mock<ICustomersService>();

            var customers = new List<CustomerDto>
            {
                new CustomerDto {
                    Id = 1,
                    Name = "Janith Perera",
                    Email = "janith@gmail.com",
                    Phone = "0711506979",
                    IsActive = true
                },
                new CustomerDto {
                    Id = 2,
                    Name = "Charith Wichramasighe",
                    Email = "Charith@gmail.com",
                    Phone = "0711506989",
                    IsActive = true
                },
                new CustomerDto {
                    Id = 3,
                    Name = "Nuwan Randima",
                    Email = "nuwan@gmail.com",
                    Phone = "0711506969",
                    IsActive = true
                },
                new CustomerDto {
                    Id = 1001,
                    Name = "Rukshani Adikari",
                    Email = "rukshani@gmail.com",
                    Phone = "0712225544",
                    IsActive = true
                }
            };

            mock.Setup(m => m.GetAllCustomers()).Returns(Task.FromResult<IList<CustomerDto>>(customers));

            mock.Setup(m => m.GetCustomerById(It.IsAny<int>())).Returns((int id) => Task.FromResult<CustomerDto?>(customers.SingleOrDefault(c => c.Id == id)));

            mock.Setup(m => m.CreateCustomer(It.IsAny<CreateCustomerRequest>()))
                .Returns((CreateCustomerRequest request) =>
                {
                    if (customers.Any(c => c.Email == request.Email))
                        throw new CustomException("Customer email or phone number is already in use.", System.Net.HttpStatusCode.BadRequest);

                    return Task.FromResult<CustomerDto>(
                        new CustomerDto
                        {
                            Id = 10111,
                            Name = request.Name,
                            Email = request.Email,
                            Phone = request.Phone,
                            IsActive = true
                        });
                });

            mock.Setup(m => m.UpdateCustomer(It.IsAny<CustomerDto>()))
                .Returns((CustomerDto request) =>
                {
                    if (!customers.Any(c => c.Id == request.Id))
                        throw new CustomException("Customer not found.", System.Net.HttpStatusCode.BadRequest);

                    return Task.FromResult<CustomerDto>(new CustomerDto
                    {
                        Email = request.Email,
                        Id = request.Id,
                        IsActive = request.IsActive,
                        Name = request.Name,
                        Phone = request.Phone,
                    });
                });
      
            return mock;
        }
    }
}
