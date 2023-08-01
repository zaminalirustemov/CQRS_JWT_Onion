using CQRS_JWTApp.API.Core.Application.Features.CQRS.Commands;
using CQRS_JWTApp.API.Core.Application.Interfaces;
using CQRS_JWTApp.API.Core.Domain;
using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Handlers.CommandsHandlers
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
