using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineOrdering.API.Controllers;
using OnlineOrdering.API.Tests.Mocks;
using OnlineOrdering.Common.Exceptions;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;

namespace OnlineOrdering.API.Tests
{
    public class CustomersControllerTests
    {
        [Fact]
        public async void GetAllCustomers_MustReturnAllCustomers()
        {
            var serviceMock = MockICustomersService.GetMock();
            var customersController = new CustomersController(serviceMock.Object);

            var results =  await customersController.GetAllCustomers() as ObjectResult;

            Assert.NotNull(results);
            Assert.Equal(StatusCodes.Status200OK, results!.StatusCode);
            Assert.NotEmpty(results.Value as IList<CustomerDto>);
            Assert.IsAssignableFrom<IList<CustomerDto>>(results.Value);
            Assert.Equal(4, ((IList<CustomerDto>)results.Value!).Count);
        }

        [Fact]
        public async void GetCustomerById_MustReturnCustomerAccordingToTheId()
        {
            var serviceMock = MockICustomersService.GetMock();
            var customersController = new CustomersController(serviceMock.Object);

            var results = await customersController.GetCustomerById(2) as ObjectResult;

            Assert.NotNull(results);
            Assert.Equal(StatusCodes.Status200OK, results!.StatusCode);
            Assert.NotNull(results.Value as CustomerDto);
            Assert.IsAssignableFrom<CustomerDto?>(results.Value);
            Assert.Equal(2, ((CustomerDto?)results.Value)!.Id);
        }

        [Fact]
        public async void GetCustomerById_MustReturnNullWhenIdIsNotAvailable()
        {
            var serviceMock = MockICustomersService.GetMock();
            var customersController = new CustomersController(serviceMock.Object);

            var results = await customersController.GetCustomerById(500) as ObjectResult;

            Assert.NotNull(results);
            Assert.Equal(StatusCodes.Status200OK, results!.StatusCode);
            Assert.Null(results.Value);
        }

        [Fact]
        public async void CreateCustomer_MustReturnNewlyCreatedCustomerRecord()
        {
            var serviceMock = MockICustomersService.GetMock();
            var customersController = new CustomersController(serviceMock.Object);

            var request = new CreateCustomerRequest 
            { 
                Email = "romani@gmail.com",
                IsActive = true,
                Name = "Romani Roopasinghe",
                Phone = "0785423366"
            };

            var results = await customersController.CreateCustomer(request) as ObjectResult;

            Assert.NotNull(results);
            Assert.Equal(StatusCodes.Status200OK, results!.StatusCode);
            Assert.IsAssignableFrom<CustomerDto>(results.Value);
            Assert.Equal(request.Email, ((CustomerDto)results.Value!).Email);
            Assert.Equal(request.Phone, ((CustomerDto)results.Value!).Phone);
        }

        [Fact]
        public void CreateCustomer_ShouldThrowAnExceptionWithExistingEmail()
        {
            var serviceMock = MockICustomersService.GetMock();
            var customersController = new CustomersController(serviceMock.Object);

            var request = new CreateCustomerRequest
            {
                Email = "rukshani@gmail.com",
                IsActive = true,
                Name = "Rukshani Adikari",
                Phone = "0785423366"
            };


            Assert.ThrowsAny<Exception>(() => customersController.CreateCustomer(request).Wait());
        }

        [Fact]
        public async void UpdateCustomer_MustReturnUpdatedCustomer()
        {
            var serviceMock = MockICustomersService.GetMock();
            var customersController = new CustomersController(serviceMock.Object);

            var request = new CustomerDto
            {
                Id = 1,
                Email = "romani@gmail.com",
                IsActive = true,
                Name = "Romani Roopasinghe",
                Phone = "0785423366"
            };

            var results = await customersController.UpdateCustomer(request) as ObjectResult;

            Assert.NotNull(results);
            Assert.Equal(StatusCodes.Status200OK, results!.StatusCode);
            Assert.IsAssignableFrom<CustomerDto>(results.Value);
            Assert.Equal(request.Email, ((CustomerDto)results.Value!).Email);
            Assert.Equal(request.Phone, ((CustomerDto)results.Value!).Phone);
            Assert.Equal(request.IsActive, ((CustomerDto)results.Value!).IsActive);
            Assert.Equal(request.Name, ((CustomerDto)results.Value!).Name);
        }

        [Fact]
        public async void UpdateCustomer_MustThrowAnExceptionWhenIdIsUnknown()
        {
            var serviceMock = MockICustomersService.GetMock();
            var customersController = new CustomersController(serviceMock.Object);

            var request = new CustomerDto
            {
                Id = 100,
                Email = "romani@gmail.com",
                IsActive = true,
                Name = "Romani Roopasinghe",
                Phone = "0785423366"
            };

            Assert.ThrowsAny<Exception>(() => customersController.UpdateCustomer(request).Wait());
        }
    }
}
