using MediatR;

namespace JwtApp.Application.Features.CQRS.Commands
{
    public class RemoveProductCommandRequest : IRequest
    {
        public RemoveProductCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
