using CQRS_JWTApp.API.Core.Application.Dto;
using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Queries
{
    public class GetCategoryQueryRequest : IRequest<CategoryListDto>
    {
        public GetCategoryQueryRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
