using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Command.DisableAgent;
using NARE.Application.Agent.Command.UpdateAgent;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Domain.Exceptions.Agent;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.DisableAgent
{
    public class DisableAgentTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<ILogger<DisableAgentCommandHandler>> Logger { get; }
        public DisableAgentCommandHandler Handler { get; }

        public DisableAgentTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Logger = new Mock<ILogger<DisableAgentCommandHandler>>();
            Handler = new DisableAgentCommandHandler(Mediator.Object, Logger.Object);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task DisableAgent_ThrowsInvalidAgentException(string agentId)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByIdQuery>(), default(CancellationToken))).ReturnsAsync((NARE.Domain.Entities.Agent) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidAgentException>(() => Handler.Handle(new DisableAgentCommand(agentId), CancellationToken.None));
        }

        [Theory]
        [InlineData("1234567890", "user@domain.com")]
        [InlineData("0987654321", "test@test.ca")]
        public async Task DisableAgent_ReturnsTrue(string agentId, string email)
        {
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Id = agentId,
                Email = email,
                AccountEnabled = false
            };
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByIdQuery>(), default(CancellationToken))).ReturnsAsync(requestedAgent);
            Mediator.Setup(m => m.Send(It.IsAny<UpdateAgentCommand>(), default(CancellationToken))).ReturnsAsync(true);
            // Act
            var result = await Handler.Handle(new DisableAgentCommand(agentId), CancellationToken.None);
            // Assert
            Assert.True(result);
        }
    }
}
