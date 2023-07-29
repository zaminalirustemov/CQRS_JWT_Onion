using CQRS_JWTApp.API.Core.Application.Dto;
using CQRS_JWTApp.API.Core.Application.Features.CQRS.Commands;
using CQRS_JWTApp.API.Core.Application.Features.CQRS.Queries;
using CQRS_JWTApp.API.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_JWTApp.API.Controllers
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
