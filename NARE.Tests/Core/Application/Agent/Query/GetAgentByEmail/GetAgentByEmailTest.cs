using System;
using System.Threading;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAgentByEmail
{
    public class GetAgentByEmailTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public GetAgentByEmailQueryHandler Handler { get; }

        public GetAgentByEmailTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new GetAgentByEmailQueryHandler(Context);
        }

        [Theory]
        [InlineData("test@test.ca", "test-user", "123")]
        public void GetAgentByEmail_ReturnsExpectedAgent(string email, string userName, string id)
        {
            // Arrange
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Email = email,
                UserName = userName,
                Id = id
            };
            // Act
            var returnedAgent = Handler.Handle(new GetAgentByEmailQuery(requestedAgent.Email), CancellationToken.None).Result;
            // Assert
            Assert.Equal(requestedAgent.Email, returnedAgent.Email);
            Assert.Equal(requestedAgent.UserName, returnedAgent.UserName);
            Assert.Equal(requestedAgent.Id, returnedAgent.Id);
        }

        [Fact]
        public void GetAgentByEmail_NullAgentReturnsNull()
        {
            // Act
            var returnedAgent = Handler.Handle(new GetAgentByEmailQuery(null), CancellationToken.None).Result;
            // Assert
            Assert.Null(returnedAgent);
        }

        [Theory]
        [InlineData("user@test.ca")]
        [InlineData("user@domain.com")]
        public void GetAgentByEmail_InvalidAgentReturnsNull(string email)
        {
            // Act
            var returnedAgent = Handler.Handle(new GetAgentByEmailQuery(email), CancellationToken.None).Result;
            // Assert
            Assert.Null(returnedAgent);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}