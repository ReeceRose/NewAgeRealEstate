using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Command.ResetPassword;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Domain.Exceptions.Account;
using NARE.Domain.Exceptions.Agent;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.ResetPassword
{
    public class ResetPasswordTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockUserManager> UserManager { get; }
        public Mock<ILogger<ResetPasswordCommandHandler>> Logger { get; set; }
        public ResetPasswordCommandHandler Handler { get; }

        public ResetPasswordTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            UserManager = new Mock<MockUserManager>();
            Logger = new Mock<ILogger<ResetPasswordCommandHandler>>();
            Handler = new ResetPasswordCommandHandler(Mediator.Object, UserManager.Object, Logger.Object);
        }

        // Valid
        [Theory]
        [InlineData("test@test.ca", "", "v6nQZlSB3Ru2ICZBhUA/4g==")]
        [InlineData("user@domain.com", "KwySES16QZ7Jicg8XprasQ==", "Password1!")]
        public void ResetPassword_ValidPasswordReset(string email, string token, string newPassword)
        {
            // Arrange
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedAgent);
            UserManager.Setup(u => u.ResetPasswordAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = Handler.Handle(new ResetPasswordCommand(token, email, newPassword), CancellationToken.None).Result;
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("test@test.ca", "", "v6nQZlSB3Ru2ICZBhUA/4g==")]
        [InlineData("user@domain.com", "KwySES16QZ7Jicg8Xpr+sQ==", "Password1!")]
        public async Task ResetPassword_InvalidAgent(string email, string token, string newPassword)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).ReturnsAsync((NARE.Domain.Entities.Agent) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidAgentException>(() => Handler.Handle(new ResetPasswordCommand(token, email, newPassword), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "", "v6nQZlSB3Ru2ICZBhUA/4g==")]
        [InlineData("user@domain.com", "KwySES16QZ7Jicg8Xpr+sQ==", "Password1!")]
        public async Task ResetPassword_ThrowsFailedToResetPasswordException(string email, string token, string newPassword)
        {
            // Arrange
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedAgent);
            UserManager.Setup(u => u.ResetPasswordAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<FailedToResetPassword>(() => Handler.Handle(new ResetPasswordCommand(token, email, newPassword), CancellationToken.None));
        }
    }
}