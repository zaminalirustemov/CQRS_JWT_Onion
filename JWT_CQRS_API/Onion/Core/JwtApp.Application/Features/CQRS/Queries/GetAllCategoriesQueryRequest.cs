using JwtApp.Application.Dto;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Queries
{
    public class GetAllCategoriesQueryRequest : IRequest<List<CategoryListDto>>
    {
    }
}
