using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Query.GenerateLoginToken;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Application.Agent.Query.LoginAgent;
using NARE.Application.Utilities;
using NARE.Domain.Exceptions;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.LoginAgent
{
    public class LoginAgentTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockSignInManager> SignInManager { get; }
        public Mock<MockUserManager> UserManager { get; }
        public IMapper Mapper { get; }
        public Mock<ILogger<LoginAgentQueryHandler>> Logger { get; }
        public LoginAgentQueryHandler Handler { get; }

        public LoginAgentTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            SignInManager = new Mock<MockSignInManager>();
            UserManager = new Mock<MockUserManager>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Logger = new Mock<ILogger<LoginAgentQueryHandler>>();
            Handler = new LoginAgentQueryHandler(Mediator.Object, SignInManager.Object, UserManager.Object, Mapper, Logger.Object);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!", "1234567890")]
        [InlineData("user@domain.com", "Password!1f4", "0987654321")]
        public void LoginAgent_ReturnsValidToken(string email, string password, string token)
        {
            // Arrange
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Email = email,
                AccountEnabled = true
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).Returns(Task.FromResult(requestedAgent));
            SignInManager
                .Setup(s => s.CheckPasswordSignInAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Success));
            Mediator.Setup(m => m.Send(It.IsAny<GenerateLoginTokenQuery>(), default(CancellationToken))).Returns(Task.FromResult(token));
            // Act
            var returnedToken = Handler.Handle(new LoginAgentQuery(email, password), default(CancellationToken)).Result;
            // Assert
            Assert.Equal(token, returnedToken);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task LoginAgent_ThrowsInvalidCredentialExceptionWhenInvalidCredentials(string email, string password)
        {
            // Arrange
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Email = email,
                AccountEnabled = true
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken)))
                .ReturnsAsync(requestedAgent);
            SignInManager
                .Setup(s => s.CheckPasswordSignInAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidCredentialException>(() =>
                Handler.Handle(new LoginAgentQuery(email, password), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task LoginAgent_ThrowsInvalidCredentialExceptionWhenAgentNotFound(string email, string password)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).Returns(Task.FromResult((NARE.Domain.Entities.Agent) null));
            // Act / Assert
            await Assert.ThrowsAsync<InvalidCredentialException>(() => Handler.Handle(new LoginAgentQuery(email, password), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task LoginAgent_ThrowsAccountLockedException(string email, string password)
        {
            // Arrange
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedAgent);
            SignInManager
                .Setup(s => s.CheckPasswordSignInAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.LockedOut);
            // Act / Assert
            // Act / Assert
            await Assert.ThrowsAsync<AccountLockedException>(() =>
                Handler.Handle(new LoginAgentQuery(email, password), CancellationToken.None));
        }
    
    }
}