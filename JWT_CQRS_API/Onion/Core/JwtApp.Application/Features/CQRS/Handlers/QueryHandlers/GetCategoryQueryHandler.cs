using AutoMapper;
using JwtApp.Application.Dto;
using JwtApp.Application.Features.CQRS.Queries;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.QueryHandlers
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
