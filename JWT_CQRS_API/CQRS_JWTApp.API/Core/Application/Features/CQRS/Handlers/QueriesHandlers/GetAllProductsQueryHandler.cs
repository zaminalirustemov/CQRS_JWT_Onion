using AutoMapper;
using CQRS_JWTApp.API.Core.Application.Dto;
using CQRS_JWTApp.API.Core.Application.Features.CQRS.Queries;
using CQRS_JWTApp.API.Core.Application.Interfaces;
using CQRS_JWTApp.API.Core.Domain;
using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Handlers.QueriesHandlers
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
