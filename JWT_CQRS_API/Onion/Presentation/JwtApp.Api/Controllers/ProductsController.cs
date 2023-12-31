﻿using JwtApp.Application.Dto;
using JwtApp.Application.Features.CQRS.Commands;
using JwtApp.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JwtApp.Api.Controllers
{
    [Authorize(Roles = "SuperAdmin,Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<ProductListDto> productListDtos = await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(productListDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ProductListDto productListDto = await _mediator.Send(new GetProductQueryRequest(id));
            return productListDto == null ? NotFound() : Ok(productListDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _mediator.Send(new RemoveProductCommandRequest(id));
            return Ok();
        }
    }
}
