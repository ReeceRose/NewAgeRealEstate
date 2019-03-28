using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NARE.Application.Agent.Query.GetAgentCount;
using NARE.Application.Agent.Query.GetAllAgents;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAgentCount
{
    public class GetAgentCountTest
    {
        public Mock<IMediator> Mediator { get; }
        public GetAgentCountQueryHandler Handler { get; }

        public GetAgentCountTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Handler = new GetAgentCountQueryHandler(Mediator.Object);
        }

        [Fact]
        public async Task GetAgentCount_ReturnsZero()
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAllAgentsQuery>(), default(CancellationToken))).ReturnsAsync((List<NARE.Domain.Entities.Agent>) null);
            // Act
            var result = await Handler.Handle(new GetAgentCountQuery(), CancellationToken.None);
            // Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(10000)]
        public async Task GetAgentCount_ReturnsValidCount(int agentCount)
        {
            // Arrange
            var agents = new List<NARE.Domain.Entities.Agent>();
            for (var i = 0; i < agentCount; i++)
            {
                agents.Add(new NARE.Domain.Entities.Agent());
            }

            Mediator.Setup(m => m.Send(It.IsAny<GetAllAgentsQuery>(), default(CancellationToken))).ReturnsAsync(agents);
            // Act
            var result = await Handler.Handle(new GetAgentCountQuery(), CancellationToken.None);
            // Assert
            Assert.Equal(result, agents.Count);
        }
    }
}
