using AutoMapper;
using MediatR;
using XProject.API.Wrappers;
using XProject.Core.DTOs;
using XProject.Core.Interfaces;
using XProject.Infrastructure.Interfaces;

namespace XProject.API.Commands.Security
{
    public class SecurityRegisterCommand : IRequest<Response<SecurityDto>>
    {
        public SecurityDto security { get; set; }

        public class SecurityResgisterCommandHandler : IRequestHandler<SecurityRegisterCommand, Response<SecurityDto>>
        {
            private readonly IMapper _mapper;
            private readonly IAuthorizationService _authorizationService;
            private readonly IPasswordService _passwordService;

            public SecurityResgisterCommandHandler(IMapper mapper, IAuthorizationService authorizationService, IPasswordService passwordService)
            {
                _mapper = mapper;
                _authorizationService = authorizationService;
                _passwordService = passwordService;
            }

            public async Task<Response<SecurityDto>> Handle(SecurityRegisterCommand request, CancellationToken cancellationToken)
            {
                var security = _mapper.Map<Core.Entities.Security>(request.security);
                security.Password = _passwordService.Hash(security.Password);

                await _authorizationService.RegisterUser(security);

                var securityDto = _mapper.Map<SecurityDto>(security);

                return new Response<SecurityDto>(securityDto);
            }
        }
    }
}
