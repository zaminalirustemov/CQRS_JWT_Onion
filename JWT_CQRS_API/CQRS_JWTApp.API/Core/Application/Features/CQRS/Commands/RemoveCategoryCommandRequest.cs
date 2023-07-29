using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Commands
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
