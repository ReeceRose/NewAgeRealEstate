using System.Linq;
using NARE.Application.Agent.Command.RemoveAgentClaim;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.RemoveAgentClaim
{
    public class RemoveAgentClaimValidationTest
    {
        public RemoveAgentClaimValidator Validator { get; set; }

        public RemoveAgentClaimValidationTest()
        {
            // Arrange
            Validator = new RemoveAgentClaimValidator(); ;
        }

        [Theory]
        [InlineData("test-user")]
        [InlineData("user@domain.com")]
        public void RemoveAgentClaim_AgentIsValid(string userName)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                UserName = userName,
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new RemoveAgentClaimCommand(agent, "key"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void RemoveAgentClaim_AgentIsInvalid()
        {
            // Act
            var result = Validator.Validate(new RemoveAgentClaimCommand(null, "key"));
            // Assert
            Assert.Contains("Agent required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("key")]
        [InlineData("role")]
        public void RemoveAgentClaim_KeyIsValid(string key)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                Email = "user@test.com",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new RemoveAgentClaimCommand(agent, key));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void RemoveAgentClaim_KeyIsInvalid(string key)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                UserName = "test-user",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new RemoveAgentClaimCommand(agent, key));
            // Assert
            Assert.Contains("Key required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
