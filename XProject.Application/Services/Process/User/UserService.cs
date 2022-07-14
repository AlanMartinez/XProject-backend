using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using XProject.Application.Interfaces;
using XProject.Core.Interfaces;
using XUser = XProject.Core.Entities.User;

namespace XProject.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<XUser> GetCurrentUser()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var user = await _unitOfWork.SecurityRepository.GetUserBy(userName);

            return user.User;
        }
    }
}