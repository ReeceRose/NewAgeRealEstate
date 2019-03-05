using System.Threading;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Query.GenerateResetPassword.Email;
using NARE.Application.Agent.Query.GenerateResetPassword.Token;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Application.Interfaces;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GenerateResetPassword.Email
{
    public class GenerateResetPasswordEmailTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<INotificationService> NotificationService { get; }
        public Mock<IConfiguration> Configuration { get; }
        public Mock<ILogger<GenerateResetPasswordEmailQueryHandler>> Logger { get; }
        public GenerateResetPasswordEmailQueryHandler Handler { get; }

        public GenerateResetPasswordEmailTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            NotificationService = new Mock<INotificationService>();
            Configuration = new Mock<IConfiguration>();
            Configuration.SetupGet(x => x["FrontEndUrl"]).Returns("url.com");
            Logger = new Mock<ILogger<GenerateResetPasswordEmailQueryHandler>>();
            Handler = new GenerateResetPasswordEmailQueryHandler(Mediator.Object, NotificationService.Object, Configuration.Object, Logger.Object);
        }
        // Email sent
        [Theory]
        [InlineData("test@test.ca", "1234567890")]
        [InlineData("user@domain.com", "9876543210")]
        public void GenerateResetPasswordEmail_EmailSent(string email, string token)
        {
            // Arrange
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Email = email
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedAgent);
            Mediator.Setup(m => m.Send(It.IsAny<GenerateResetPasswordTokenQuery>(), default(CancellationToken))).ReturnsAsync(token);
            NotificationService.Setup(n => n.SendNotificationAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            // Act
            var result = Handler.Handle(new GenerateResetPasswordEmailQuery(email), CancellationToken.None).Result;
            // Assert
            Assert.Equal(token, result);
        }

        // Null on invalid agent
        [Theory]
        [InlineData("test@test.ca")]
        [InlineData(null)]
        public void GenerateResetPasswordEmail_ReturnsNullOnInvalidAgent(string email)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).ReturnsAsync((NARE.Domain.Entities.Agent) null);
            // Act
            var result = Handler.Handle(new GenerateResetPasswordEmailQuery(email), default(CancellationToken)).Result;
            // Assert
            Assert.Null(result);
        }
    }
}