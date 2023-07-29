using CQRS_JWTApp.API.Core.Application.Dto;
using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Queries
{
    public class GetAllCategoriesQueryRequest : IRequest<List<CategoryListDto>>
    {
    }
}
