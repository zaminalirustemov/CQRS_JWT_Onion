using MediatR;

namespace JwtApp.Application.Features.CQRS.Commands
{
    public class CreateProductCommandRequest : IRequest
    {
        public int CategoryId { get; set; }

        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
