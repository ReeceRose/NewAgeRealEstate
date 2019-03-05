using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Command.AddAgentClaim;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.AddAgentClaim
{
    public class AddAgentClaimTest
    {
        public Mock<MockUserManager> UserManager { get; }
        public Mock<ILogger<AddAgentClaimCommandHandler>> Logger { get; }
        public AddAgentClaimCommandHandler Handler { get; }

        public AddAgentClaimTest()
        {
            // Arrange
            UserManager = new Mock<MockUserManager>();
            Logger = new Mock<ILogger<AddAgentClaimCommandHandler>>();
            Handler = new AddAgentClaimCommandHandler(UserManager.Object, Logger.Object);
        }

        [Theory]
        [InlineData("user@test.com", "key", "value")]
        [InlineData("user@test.com", "test", "claim")]
        public async Task AddAgentClaim_ClaimAdded(string email, string key, string value)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                Email = email
            };
            UserManager.Setup(u => u.AddClaimAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await Handler.Handle(new AddAgentClaimCommand(agent, key, value), CancellationToken.None);
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("user@test.com", null, "value")]
        [InlineData("user@test.com", "key", null)]
        public async Task AddAgentClaim_ClaimNotAdded(string email, string key, string value)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                Email = email
            };
            UserManager.Setup(u => u.AddClaimAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Handler.Handle(new AddAgentClaimCommand(agent, key, value), CancellationToken.None));
        }
    }
}
