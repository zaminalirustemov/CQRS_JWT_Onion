using JwtApp.Application.Dto;
using JwtApp.Application.Features.CQRS.Queries;
using JwtApp.Application.Interfaces;
using JwtApp.Domain.Entities;
using MediatR;

namespace JwtApp.Application.Features.CQRS.Handlers.QueryHandlers
{
    public class CheckUserQueryHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppRole> _roleRepository;

        public CheckUserQueryHandler(IRepository<AppUser> userRepository, IRepository<AppRole> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            CheckUserResponseDto checkUserResponseDto = new();
            var appUser = await _userRepository.GetByFilterAsync(x => x.Username == request.Username && x.Password == request.Password);
            if (appUser != null)
            {
                var appRole = await _roleRepository.GetByFilterAsync(x => x.Id == appUser.AppRoleId);

                checkUserResponseDto.Id = appUser.Id;
                checkUserResponseDto.Username = appUser.Username;
                checkUserResponseDto.isExist = true;
                checkUserResponseDto.Role = appRole?.Definition;
            }
            else
            {
                checkUserResponseDto.isExist = false;
            }
            return checkUserResponseDto;
        }
    }

}
