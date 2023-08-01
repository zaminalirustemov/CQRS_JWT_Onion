using AutoMapper;
using CQRS_JWTApp.API.Core.Application.Dto;
using CQRS_JWTApp.API.Core.Application.Features.CQRS.Queries;
using CQRS_JWTApp.API.Core.Application.Interfaces;
using CQRS_JWTApp.API.Core.Domain;
using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Handlers.QueriesHandlers
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, CategoryListDto>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CategoryListDto> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByFilterAsync(x => x.Id == request.Id);
            CategoryListDto categoryListDto = _mapper.Map<CategoryListDto>(category);
            return categoryListDto;
        }
    }
}
