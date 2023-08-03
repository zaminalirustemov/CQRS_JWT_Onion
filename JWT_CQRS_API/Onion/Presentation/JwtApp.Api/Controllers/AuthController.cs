using JwtApp.Application.Dto;
using JwtApp.Application.Features.CQRS.Commands;
using JwtApp.Application.Features.CQRS.Queries;
using JwtApp.Application.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(CheckUserQueryRequest request)
        {
            CheckUserResponseDto checkUserResponseDto = await _mediator.Send(request);
            if (checkUserResponseDto.isExist)
            {
                return Created("", JwtTokenGenerator.GenerateToken(checkUserResponseDto));
            }
            else
            {
                return BadRequest("Username ve ya Password yanlisdir,");
            }
        }
    }
}
