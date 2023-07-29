using AutoMapper;
using CQRS_JWTApp.API.Core.Application.Dto;
using CQRS_JWTApp.API.Core.Domain;

namespace CQRS_JWTApp.API.Core.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            this.CreateMap<CategoryListDto, Category>().ReverseMap();
        }
    }
}
