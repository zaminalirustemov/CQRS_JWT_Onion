using CQRS_JWTApp.API.Core.Application.Features.CQRS.Commands;
using CQRS_JWTApp.API.Core.Application.Interfaces;
using CQRS_JWTApp.API.Core.Domain;
using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Handlers.CommandsHandlers
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public RemoveProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            var removedProduct = await _repository.GetByIdAsync(request.Id);
            if (removedProduct != null) await _repository.RemoveAsync(removedProduct);
            return Unit.Value;
        }
    }
}
