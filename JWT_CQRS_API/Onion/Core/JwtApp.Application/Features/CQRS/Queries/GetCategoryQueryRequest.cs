using JwtApp.Application.Dto;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Queries
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
