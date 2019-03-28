using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using NARE.Application.Agent.Command.CreateAgent;
using NARE.Application.Utilities;
using NARE.Domain.Entities;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.CreateAgent
{
    public class CreateAgentTest
    {
        public Mock<MockUserManager> UserManager { get; }
        public IMapper Mapper { get; }
        public CreateAgentCommandHandler Handler { get; }

        public CreateAgentTest()
        {
            // Arrange
            UserManager = new Mock<MockUserManager>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Handler = new CreateAgentCommandHandler(UserManager.Object, Mapper);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task CreateAgent_AgentCreated(string email, string password)
        {
            // Arrange
            var agent = new AgentDto()
            {
                Email = email
            };
            UserManager.Setup(u => u.CreateAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await Handler.Handle(new CreateAgentCommand(agent, password), CancellationToken.None);
            // Assert
            Assert.True(result.Succeeded);
        }
        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task CreateAgent_AgentNotCreated(string email, string password)
        {
            // Arrange
            var agent = new AgentDto()
            {
                Email = email
            };
            UserManager.Setup(u => u.CreateAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            // Act
            var result = await Handler.Handle(new CreateAgentCommand(agent, password), CancellationToken.None);
            // Assert
            Assert.False(result.Succeeded);
        }
    }
}
