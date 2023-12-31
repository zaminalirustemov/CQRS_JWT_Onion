﻿using JwtApp.Application.Dto;
using JwtApp.Application.Features.CQRS.Commands;
using JwtApp.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JwtApp.Api.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<CategoryListDto> categoryListDtos = await _mediator.Send(new GetAllCategoriesQueryRequest());
            return Ok(categoryListDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            CategoryListDto categoryListDto = await _mediator.Send(new GetCategoryQueryRequest(id));
            return categoryListDto == null ? NotFound() : Ok(categoryListDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryCommandRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _mediator.Send(new RemoveCategoryCommandRequest(id));
            return Ok();
        }

    }
}
