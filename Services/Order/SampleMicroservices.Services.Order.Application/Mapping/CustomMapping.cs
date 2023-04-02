using AutoMapper;
using SampleMicroservices.Services.Order.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMicroservices.Services.Order.Application.Mapping
{
    public class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<Domain.OrderAggerate.Order, OrderDto>().ReverseMap();
            CreateMap<Domain.OrderAggerate.OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Domain.OrderAggerate.Address, AddressDto>().ReverseMap();
        }
    }
}
