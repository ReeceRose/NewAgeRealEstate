using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Command.CreateAgent;
using NARE.Application.Agent.Command.NewAgent;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Application.Utilities;
using NARE.Domain.Exceptions.Account;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.NewAgent
{
    public class NewAgentTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public Mock<ILogger<NewAgentCommandHandler>> Logger { get; }
        public NewAgentCommandHandler Handler { get; }

        public NewAgentTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.ValidateInlineMaps = false;
            }));
            Logger = new Mock<ILogger<NewAgentCommandHandler>>();
            Handler = new NewAgentCommandHandler(Mediator.Object, Mapper, Logger.Object);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public void NewAgent_SuccessfullyRegistersUser(string email, string password)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).ReturnsAsync((NARE.Domain.Entities.Agent) null);
            Mediator.Setup(m => m.Send(It.IsAny<CreateAgentCommand>(), default(CancellationToken))).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = Handler.Handle(new NewAgentCommand(email, password), CancellationToken.None).Result;
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task NewAgent_ThrowsAccountAlreadyExistsException(string email, string password)
        {
            // Arrange
            var requestedUser = new NARE.Domain.Entities.Agent() { Email = email };
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).ReturnsAsync(requestedUser);
            // Act / Assert
            await Assert.ThrowsAsync<AccountAlreadyExistsException>(() => Handler.Handle(new NewAgentCommand(email, password), CancellationToken.None));
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task NewAgent_FailedToRegistersUser(string email, string password)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAgentByEmailQuery>(), default(CancellationToken))).ReturnsAsync((NARE.Domain.Entities.Agent) null);
            Mediator.Setup(m => m.Send(It.IsAny<CreateAgentCommand>(), default(CancellationToken))).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<InvalidRegisterException>(() => Handler.Handle(new NewAgentCommand(email, password), CancellationToken.None));
        }
    }
}
