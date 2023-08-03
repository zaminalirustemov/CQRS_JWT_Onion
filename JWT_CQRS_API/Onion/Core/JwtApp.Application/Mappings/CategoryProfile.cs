using AutoMapper;
using JwtApp.Application.Dto;
using JwtApp.Domain.Entities;

namespace JwtApp.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            this.CreateMap<CategoryListDto, Category>().ReverseMap();
        }
    }
}
