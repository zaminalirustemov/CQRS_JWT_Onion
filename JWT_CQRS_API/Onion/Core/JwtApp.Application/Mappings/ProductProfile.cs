using AutoMapper;
using JwtApp.Application.Dto;
using JwtApp.Domain.Entities;

namespace JwtApp.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Product, ProductListDto>().ReverseMap();
        }
    }
}
