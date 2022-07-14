using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XProject.Core.Entities;
using XProject.Core.Exceptions;
using XProject.Core.Interfaces;
using XProject.Infrastructure.Interfaces;
using XProject.Infrastructure.Options;

namespace XProject.Application.Services.User
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly AuthenticationOptions _authOptions;

        public AuthorizationService(IUnitOfWork unitOfWork, IPasswordService passwordService, IOptions<AuthenticationOptions> authOptions)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _authOptions = authOptions.Value;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin)
        {
            var user = await _unitOfWork.SecurityRepository.GetLoginByCredentials(userLogin);
            return user;
        }

        public async Task RegisterUser(Security security)
        {
            security.CreateUser(security.UserName);
            var user = await _unitOfWork.SecurityRepository.GetUserBy(security.UserName);
            if (user != null)
                throw new ValidationException("Username already exist");

            await _unitOfWork.SecurityRepository.Add(security);
            await _unitOfWork.SaveChangesAsync();
        }

        public string GenerateToken(Security security)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey));
            var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signinCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, security.UserName),
                //new Claim(ClaimTypes.Name, "XXX@XXX.com"),
                new Claim(ClaimTypes.Role, security.Role.ToString())
            };

            //Payload
            var payload = new JwtPayload
            (
                _authOptions.Issuer,
                _authOptions.Audience,
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<(bool, Security)> IsValidUser(UserLogin userLogin)
        {
            var user = await GetLoginByCredentials(userLogin);
            var isValid = _passwordService.Check(user?.Password, userLogin.Password);

            return (isValid, user);
        }
    }
}
