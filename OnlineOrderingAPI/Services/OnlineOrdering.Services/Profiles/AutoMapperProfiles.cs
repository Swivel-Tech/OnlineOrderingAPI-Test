using AutoMapper;
using OnlineOrdering.Common.Models.Dtos;
using OnlineOrdering.Common.Models.Requests;
using OnlineOrdering.Data.DatabaseContext;

namespace OnlineOrdering.Services.Profiles
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequest, Product>();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerRequest, Customer>();

            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            CreateMap<CreateOrderHeaderRequest, OrderHeader>()
                .ForMember(
                src => src.Status, 
                act => act.MapFrom(dest => dest.Status.ToString()));
            
            CreateMap<OrderLineItem, OrderLineItemDto>().ReverseMap();
            CreateMap<CreateOrderLineItemRequest, OrderLineItem>();
        }
    }
}
