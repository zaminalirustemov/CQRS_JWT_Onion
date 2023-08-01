using CQRS_JWTApp.API.Core.Application.Features.CQRS.Commands;
using CQRS_JWTApp.API.Core.Application.Interfaces;
using CQRS_JWTApp.API.Core.Domain;
using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Handlers.CommandsHandlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;

        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedCategory = await _repository.GetByIdAsync(request.Id);
            if (updatedCategory is not null)
            {
                updatedCategory.Definition = request.Definition;
                await _repository.UpdateAsync(updatedCategory);
            }
            return Unit.Value;
        }
    }
}
