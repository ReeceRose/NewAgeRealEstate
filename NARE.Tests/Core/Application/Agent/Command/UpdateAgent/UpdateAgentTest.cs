using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NARE.Application.Agent.Command.UpdateAgent;
using NARE.Domain.Exceptions;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.UpdateAgent
{
    public class UpdateAgentTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public UpdateAgentCommandHandler Handler { get; }

        public UpdateAgentTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new UpdateAgentCommandHandler(Context);
        }

        [Fact]
        public async Task UpdateAgent_HandlesNull()
        {
            // Act / Assert
            await Assert.ThrowsAsync<InvalidAgentException>(() => Handler.Handle(new UpdateAgentCommand(null), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "user@domain.com")]
        public async Task UpdateAgent_UpdatesUser(string email, string newEmail)
        {
            var agent = Context.Users.First(u => u.Email == email);
            agent.Email = newEmail;
            // Act
            var result = await Handler.Handle(new UpdateAgentCommand(agent), CancellationToken.None);
            // Assert
            Assert.True(result);
            Assert.Equal(Context.Users.First().Email, newEmail);
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }
}
