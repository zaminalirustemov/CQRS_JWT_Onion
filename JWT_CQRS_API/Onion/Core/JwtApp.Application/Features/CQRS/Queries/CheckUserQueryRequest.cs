using JwtApp.Application.Dto;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Queries
{
    public class CheckUserQueryRequest : IRequest<CheckUserResponseDto>
    {
        public string? Username { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }
}
