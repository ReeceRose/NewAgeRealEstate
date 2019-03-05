using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using AutoMapper;
using Moq;
using NARE.Application.Agent.Model;
using NARE.Application.Agent.Query.GetAgentClaim;
using NARE.Application.Utilities;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAgentClaim
{
    public class GetAgentClaimTest
    {
        public Mock<MockUserManager> UserManager { get; }
        public IMapper Mapper { get; }
        public GetAgentClaimQueryHandler Handler { get; }

        public GetAgentClaimTest()
        {
            // Arrange
            UserManager = new Mock<MockUserManager>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Handler = new GetAgentClaimQueryHandler(UserManager.Object, Mapper);
        }

        [Theory]
        [InlineData("key", "value")]
        [InlineData("role", "Admin")]
        public void GetAgentClaim_ReturnsAClaim(string key, string value)
        {
            // Arrange
            var agent = new AgentDto()
            {
                Email = "user@test.com"
            };
            var claims = new List<Claim>()
            {
                new Claim(key, value)
            };
            UserManager.Setup(u => u.GetClaimsAsync(It.IsAny<NARE.Domain.Entities.Agent>())).ReturnsAsync(claims);
            // Act
            var returnedClaims = Handler.Handle(new GetAgentClaimQuery(agent), CancellationToken.None).Result;
            // Assert
            Assert.Single(returnedClaims);
        }

        [Theory]
        [InlineData("key", "value")]
        [InlineData("role", "Admin")]
        public void GetAgentClaim_ReturnsClaims(string key, string value)
        {
            // Arrange
            var agent = new AgentDto()
            {
                Email = "user@test.com"
            };
            var claims = new List<Claim>()
            {
                new Claim(key, value),
                new Claim(key, value),
                new Claim(key, value)
            };
            UserManager.Setup(u => u.GetClaimsAsync(It.IsAny<NARE.Domain.Entities.Agent>())).ReturnsAsync(claims);
            // Act
            var returnedClaims = Handler.Handle(new GetAgentClaimQuery(agent), CancellationToken.None).Result;
            // Assert
            Assert.Equal(3, returnedClaims.Count);
        }

        [Fact]
        public void GetAgentClaim_ReturnsEmpty()
        {
            // Arrange
            var agent = new AgentDto()
            {
                Email = "user@test.com"
            };
            var claims = new List<Claim>();

            UserManager.Setup(u => u.GetClaimsAsync(It.IsAny<NARE.Domain.Entities.Agent>())).ReturnsAsync(claims);
            // Act
            var returnedClaims = Handler.Handle(new GetAgentClaimQuery(agent), CancellationToken.None).Result;
            // Assert
            Assert.Empty(returnedClaims);
        }
    }
}
