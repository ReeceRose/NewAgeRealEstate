using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Command.EnableAgent;
using NARE.Application.Agent.Command.UpdateAgent;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Domain.Exceptions.Agent;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.EnableAgent
{
    public class EnableAgentTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<ILogger<EnableAgentCommandHandler>> Logger { get; }
        public EnableAgentCommandHandler Handler { get; }

        public EnableAgentTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Logger = new Mock<ILogger<EnableAgentCommandHandler>>();
            Handler = new EnableAgentCommandHandler(Mediator.Object, Logger.Object);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task EnableAgent_ThrowsInvalidAgentException(string agentId)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByIdQuery>(), default(CancellationToken)))
                .ReturnsAsync((NARE.Domain.Entities.Agent) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidAgentException>(() =>
                Handler.Handle(new EnableAgentCommand(agentId), CancellationToken.None));
        }

        [Theory]
        [InlineData("1234567890", "user@domain.com")]
        [InlineData("0987654321", "test@test.ca")]
        public async Task EnableAgent_ReturnsTrue(string agentId, string email)
        {
            // Arrange
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Id = agentId,
                Email = email,
                AccountEnabled = false
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByIdQuery>(), default(CancellationToken)))
                .ReturnsAsync(requestedAgent);
            Mediator.Setup(m => m.Send(It.IsAny<UpdateAgentCommand>(), default(CancellationToken))).ReturnsAsync(true);
            // Act
            var result = await Handler.Handle(new EnableAgentCommand(agentId), CancellationToken.None);
            // Assert
            Assert.True(result);
        }
    }
}