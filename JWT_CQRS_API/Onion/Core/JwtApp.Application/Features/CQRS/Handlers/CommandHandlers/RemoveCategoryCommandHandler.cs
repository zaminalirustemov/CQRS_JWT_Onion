using JwtApp.Application.Features.CQRS.Commands;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.CommandHandlers
{
    public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;

        public RemoveCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var removedCategory = await _repository.GetByIdAsync(request.Id);
            if (removedCategory != null) await _repository.RemoveAsync(removedCategory);
            return Unit.Value;
        }
    }
}
