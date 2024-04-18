using AutoMapper;
using Entities;
using Entities.Dto;

namespace PruebaAPI.Mapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper() 
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
        }
    }
}
