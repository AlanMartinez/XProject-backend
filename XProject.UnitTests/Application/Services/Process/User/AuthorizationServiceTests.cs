using Microsoft.AspNetCore.Authentication;
using AuthenticationOptions = XProject.Infrastructure.Options.AuthenticationOptions;
using Moq;
using XProject.Application.Services.User;
using XProject.Core.Interfaces;
using XProject.Infrastructure.Interfaces;
using Xunit;
using Microsoft.Extensions.Options;
using XProject.Core.Entities;
using System.Threading.Tasks;
using System;

namespace XProject.UnitTests.Application.Services.Process.User
{
    public class AuthorizationServiceTests
    {
        [Fact]
        public void RegisterUser_ValidRegister_OK()
        {
            var sut = GetSut(out var unitOfWorkMock,
                            out var _,
                            out var _);

            unitOfWorkMock.Setup(x => x.SecurityRepository.GetUserBy(It.IsAny<string>()));
            unitOfWorkMock.Setup(x => x.SecurityRepository.Add(It.IsAny<Security>()));
            unitOfWorkMock.Setup(x => x.SaveChangesAsync());

            var request = new Security
            {
                UserName = "prueba@prueba.com",
                Password = "string"
            };

            var response = sut.RegisterUser(request);

            unitOfWorkMock.Verify(x => x.SecurityRepository.GetUserBy(It.IsAny<string>()), Times.Once);
            unitOfWorkMock.Verify(x => x.SecurityRepository.Add(It.IsAny<Security>()), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        private Task<Security> GetNewUser()
        {
            var sec = new Security { UserName = "alan@alan.com" };
            return Task.FromResult<Security>(sec);
        }

        private IAuthorizationService GetSut(out Mock<IUnitOfWork> unitOfWorkMock,
                                             out Mock<IPasswordService> passwordServiceMock,
                                             out Mock<IOptions<AuthenticationOptions>> authOptionsMock)
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            passwordServiceMock = new Mock<IPasswordService>();
            authOptionsMock = new Mock<IOptions<AuthenticationOptions>>();
            return new AuthorizationService(unitOfWorkMock.Object, passwordServiceMock.Object, authOptionsMock.Object);
        }
    }
}
