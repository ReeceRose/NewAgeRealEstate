using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NARE.Application.Agent.Query.GetAllAgents;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAllAgents
{
    public class GetAllAgentsTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public GetAllAgentsQueryHandler Handler { get; }

        public GetAllAgentsTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new GetAllAgentsQueryHandler(Context);
        }

        [Fact]
        public async Task GetAllAgents_ReturnsZero()
        {
            // Arrange
            // Context by default has one agent, so remove it
            Context.Remove(Context.Users.First());
            Context.SaveChanges();
            // Act
            var result = await Handler.Handle(new GetAllAgentsQuery(), CancellationToken.None);
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAgents_ReturnsValidAgentCount()
        {
            // Arrange / Act
            var result = await Handler.Handle(new GetAllAgentsQuery(), CancellationToken.None);
            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }
}
