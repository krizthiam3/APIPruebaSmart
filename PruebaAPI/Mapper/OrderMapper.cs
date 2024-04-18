using AutoMapper;
using Entities;
using Entities.Dto;

namespace PruebaAPI.Mapper
{
    public class OrderMapper : Profile
    {
        public OrderMapper() 
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();
        }
    }
}
