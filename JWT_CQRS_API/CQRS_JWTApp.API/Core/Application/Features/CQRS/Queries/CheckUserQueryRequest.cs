using CQRS_JWTApp.API.Core.Application.Dto;
using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Queries
{
    public class CheckUserQueryRequest : IRequest<CheckUserResponseDto>
    {
        public string? Username { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }
}
