using MediatR;

namespace JwtApp.Application.Features.CQRS.Commands
{
    public class RemoveCategoryCommandRequest : IRequest
    {
        public RemoveCategoryCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
