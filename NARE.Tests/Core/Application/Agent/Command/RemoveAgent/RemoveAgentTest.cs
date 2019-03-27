using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Command.RemoveAgent;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Domain.Exceptions.Agent;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.RemoveAgent
{
    public class RemoveAgentTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockUserManager> UserManager { get; }
        public Mock<ILogger<RemoveAgentCommandHandler>> Logger { get; }
        public RemoveAgentCommandHandler Handler { get; }

        public RemoveAgentTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            UserManager = new Mock<MockUserManager>();
            Logger = new Mock<ILogger<RemoveAgentCommandHandler>>();
            Handler = new RemoveAgentCommandHandler(Mediator.Object, UserManager.Object, Logger.Object);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task RemoveAgentTest_ThrowsInvalidAgentException(string agentId)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByIdQuery>(), default(CancellationToken))).ReturnsAsync((NARE.Domain.Entities.Agent) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidAgentException>(() => Handler.Handle(new RemoveAgentCommand(agentId), CancellationToken.None));
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task RemoveAgentTest_RemovesSuccessfully(string agentId)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                Id = agentId
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByIdQuery>(), default(CancellationToken))).ReturnsAsync(agent);
            UserManager.Setup(u => u.DeleteAsync(It.IsAny<NARE.Domain.Entities.Agent>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await Handler.Handle(new RemoveAgentCommand(agentId), CancellationToken.None);
            // Assert
            Assert.True(result);
        }
    }
}
