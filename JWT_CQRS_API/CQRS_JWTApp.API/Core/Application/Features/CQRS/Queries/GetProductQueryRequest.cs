using CQRS_JWTApp.API.Core.Application.Dto;
using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Queries
{
    public class GetProductQueryRequest : IRequest<ProductListDto>
    {
        public GetProductQueryRequest(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
