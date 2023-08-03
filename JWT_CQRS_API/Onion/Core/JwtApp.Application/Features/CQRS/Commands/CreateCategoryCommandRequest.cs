using MediatR;

namespace JwtApp.Application.Features.CQRS.Commands
{
    public class CreateCategoryCommandRequest : IRequest
    {
        public string? Definition { get; set; }
    }
}
