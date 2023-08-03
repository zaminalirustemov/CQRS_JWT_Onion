using JwtApp.Application.Features.CQRS.Commands;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.CommandHandlers
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
