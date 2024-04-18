using AutoMapper;
using Entities;
using Entities.Dto;

namespace PruebaAPI.Mapper
{
    public class OrderDetailMapper : Profile
    {
        public OrderDetailMapper() 
        {
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailCreateDto>().ReverseMap();
        }
    }
}
