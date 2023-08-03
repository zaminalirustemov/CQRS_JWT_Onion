using JwtApp.Application.Dto;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Queries
{
    public class GetAllProductsQueryRequest : IRequest<List<ProductListDto>>
    {
    }
}
