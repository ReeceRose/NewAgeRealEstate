using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Command.UpdateAgentInformation;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.UpdateAgentInformation
{
    public class UpdateAgentInformationTest : IDisposable
    {
        public Mock<IMediator> Mediator { get; }
        public ApplicationDbContext Context { get; }
        public Mock<ILogger<UpdateAgentInformationCommandHandler>> Logger { get; }
        public UpdateAgentInformationCommandHandler Handler { get; }

        public UpdateAgentInformationTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Context = ContextFactory.Create();
            Logger = new Mock<ILogger<UpdateAgentInformationCommandHandler>>();
            Handler = new UpdateAgentInformationCommandHandler(Mediator.Object, Logger.Object);
        }

        [Theory]
        [InlineData("test@test.ca", "Random User")]
        [InlineData("test@test.ca", "User")]
        public async Task UpdateAgentInformation_UpdatesUserName(string email, string newName)
        {
            // Arrange
            var agent = Context.Users.First(a => a.Email == email);
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByIdQuery>(), default(CancellationToken))).ReturnsAsync(agent);
            agent.Name = newName;
            // Act
            await Handler.Handle(new UpdateAgentInformationCommand(agent), CancellationToken.None);
            // Assert
            Assert.Equal(Context.Users.First(a => a.Email == email).Name, newName);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
