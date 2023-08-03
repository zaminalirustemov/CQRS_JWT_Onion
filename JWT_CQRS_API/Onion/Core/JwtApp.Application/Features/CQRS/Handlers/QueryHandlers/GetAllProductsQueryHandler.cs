using AutoMapper;
using JwtApp.Application.Dto;
using JwtApp.Application.Features.CQRS.Queries;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<ProductListDto>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<ProductListDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            List<Product> products = await _repository.GetAllAsync();
            List<ProductListDto> productListDtos = _mapper.Map<List<ProductListDto>>(products);
            return productListDtos;
        }
    }
}
