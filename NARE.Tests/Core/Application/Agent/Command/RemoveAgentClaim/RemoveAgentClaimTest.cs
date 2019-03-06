using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Agent.Command.RemoveAgentClaim;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.RemoveAgentClaim
{
    public class RemoveAgentClaimTest
    {
        public Mock<MockUserManager> UserManager { get; }
        public Mock<ILogger<RemoveAgentClaimCommandHandler>> Logger { get; }
        public RemoveAgentClaimCommandHandler Handler { get; }

        public RemoveAgentClaimTest()
        {
            // Arrange
            UserManager = new Mock<MockUserManager>();
            Logger = new Mock<ILogger<RemoveAgentClaimCommandHandler>>();
            Handler = new RemoveAgentClaimCommandHandler(UserManager.Object, Logger.Object);
        }

        [Theory]
        [InlineData("user@test.com", "key")]
        [InlineData("user@test.com", "test")]
        public async Task RemoveAgentClaim_ClaimAdded(string email, string key)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                Email = email
            };
            UserManager.Setup(u => u.RemoveClaimAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await Handler.Handle(new RemoveAgentClaimCommand(agent, key), CancellationToken.None);
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("user@test.com", null)]
        public async Task RemoveAgentClaim_ClaimNotAdded(string email, string key)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                Email = email
            };
            UserManager.Setup(u => u.RemoveClaimAsync(It.IsAny<NARE.Domain.Entities.Agent>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Failed());
            // Act / Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Handler.Handle(new RemoveAgentClaimCommand(agent, key), CancellationToken.None));
        }
    }
}
