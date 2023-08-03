using JwtApp.Application.Features.CQRS.Commands;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.CommandHandlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;

        public CreateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Category
            {
                Definition = request.Definition
            });
            return Unit.Value;
        }
    }
}
