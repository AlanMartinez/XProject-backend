using AutoMapper;
using MediatR;
using XProject.API.Wrappers;
using XProject.Core.Entities;
using XProject.Core.Exceptions;
using XProject.Core.Interfaces;

namespace XProject.API.Queries.Auth
{
    public class AuthorizationQuery : IRequest<Response<string>>
    {
        public UserLogin UserLogin { get; set; }

        public class AuthorizationQueryHandler : IRequestHandler<AuthorizationQuery, Response<string>>
        {
            private readonly IAuthorizationService _authorizationService;
            private readonly IMapper _mapper;

            public AuthorizationQueryHandler(IAuthorizationService authorizationService, IMapper mapper)
            {
                _authorizationService = authorizationService;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(AuthorizationQuery request, CancellationToken cancellationToken)
            {
                var userLogin = _mapper.Map<UserLogin>(request.UserLogin);

                var validation = await _authorizationService.IsValidUser(userLogin);
                if (validation.Item1)
                {
                    var token = _authorizationService.GenerateToken(validation.Item2);
                    return new Response<string>(data: token);
                }

                throw new ValidationException("Invalid Username/Password");
            }
        }
    }
}
