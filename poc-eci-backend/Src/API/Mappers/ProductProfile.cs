using API.DTOs;
using AutoMapper;
using Domain.Entities;

namespace API.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductResponseDTO>().ReverseMap();
        }
    }
}
