using System;
using System.Threading;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAgentDtoById
{
    public class GetAgentDtoByIdTest: IDisposable
    {
        public ApplicationDbContext Context { get; }
        public GetAgentByIdQueryHandler Handler { get; }

        public GetAgentDtoByIdTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new GetAgentByIdQueryHandler(Context);
        }

        [Fact]
        public void GetAgentDtoById_ReturnsExpectedAgent()
        {
            // Arrange
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Email = "test@test.ca",
                UserName = "test-user",
                Id = "123"
            };
            // Act
            var returnedAgent = Handler.Handle(new GetAgentByIdQuery(requestedAgent.Id), CancellationToken.None).Result;
            // Assert
            Assert.Equal(requestedAgent.Email, returnedAgent.Email);
            Assert.Equal(requestedAgent.UserName, returnedAgent.UserName);
            Assert.Equal(requestedAgent.Id, returnedAgent.Id);
        }

        [Fact]
        public void GetAgentDtoById_NullAgentReturnsNull()
        {
            // Act
            var returnedAgent = Handler.Handle(new GetAgentByIdQuery(null), CancellationToken.None).Result;
            // Assert
            Assert.Null(returnedAgent);
        }

        [Theory]
        [InlineData("user@test.ca")]
        [InlineData("user@domain.com")]
        public void GetAgentDtoById_InvalidAgentReturnsNull(string email)
        {
            // Act
            var returnedAgent = Handler.Handle(new GetAgentByIdQuery(email), CancellationToken.None).Result;
            // Assert
            Assert.Null(returnedAgent);
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }
}