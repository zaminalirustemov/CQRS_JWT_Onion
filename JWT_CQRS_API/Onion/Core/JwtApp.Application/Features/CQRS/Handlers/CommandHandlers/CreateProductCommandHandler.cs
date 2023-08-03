using JwtApp.Application.Features.CQRS.Commands;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public CreateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Product
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price,
            });
            return Unit.Value;
        }
    }
}
