using JwtApp.Application.Features.CQRS.Commands;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.CommandHandlers
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
