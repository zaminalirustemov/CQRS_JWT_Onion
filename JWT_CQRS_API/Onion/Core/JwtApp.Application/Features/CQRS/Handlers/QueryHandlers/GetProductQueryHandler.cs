using AutoMapper;
using JwtApp.Application.Dto;
using JwtApp.Application.Features.CQRS.Queries;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.QueryHandlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, ProductListDto>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProductListDto> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByFilterAsync(x => x.Id == request.Id);
            ProductListDto productListDto = _mapper.Map<ProductListDto>(product);
            return productListDto;
        }
    }
}
