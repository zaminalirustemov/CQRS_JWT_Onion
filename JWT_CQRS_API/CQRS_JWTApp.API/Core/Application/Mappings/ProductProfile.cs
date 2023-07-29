using AutoMapper;
using CQRS_JWTApp.API.Core.Application.Dto;
using CQRS_JWTApp.API.Core.Domain;

namespace CQRS_JWTApp.API.Core.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Product, ProductListDto>().ReverseMap();
        }
    }
}
