using AutoMapper;
using JwtApp.Application.Dto;
using JwtApp.Application.Features.CQRS.Queries;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.QueryHandlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, List<CategoryListDto>>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CategoryListDto>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            List<Category> categories = await _repository.GetAllAsync();
            List<CategoryListDto> categoryListDtos = _mapper.Map<List<CategoryListDto>>(categories);
            return categoryListDtos;
        }
    }
}
