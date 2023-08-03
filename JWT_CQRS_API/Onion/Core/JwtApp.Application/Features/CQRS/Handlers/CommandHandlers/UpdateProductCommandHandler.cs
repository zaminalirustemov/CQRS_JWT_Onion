using JwtApp.Application.Features.CQRS.Commands;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedProduct = await _repository.GetByIdAsync(request.Id);
            if (updatedProduct != null)
            {
                updatedProduct.CategoryId = request.CategoryId;
                updatedProduct.Name = request.Name;
                updatedProduct.Stock = request.Stock;
                updatedProduct.Price = request.Price;
                await _repository.UpdateAsync(updatedProduct);
            }
            return Unit.Value;
        }
    }
}
