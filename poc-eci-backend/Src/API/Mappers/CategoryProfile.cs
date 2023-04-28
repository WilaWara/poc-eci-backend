using API.DTOs;
using AutoMapper;
using Domain.Entities;

namespace API.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryResponseDTO>().ReverseMap();
        }
    }
}
