using System.Linq;
using NARE.Application.Agent.Command.AddAgentClaim;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.AddAgentClaim
{
    public class AddAgentClaimValidationTest
    {
        public AddAgentClaimCommandValidator Validator { get; set; }

        public AddAgentClaimValidationTest()
        {
            // Arrange
            Validator = new AddAgentClaimCommandValidator();;
        }

        [Theory]
        [InlineData("test-user")]
        [InlineData("user@domain.com")]
        public void AddAgentClaim_AgentIsValid(string agentName)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                UserName = agentName,
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddAgentClaimCommand(agent, "key", "value"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void AddAgentClaim_AgentIsInvalid()
        {
            // Act
            var result = Validator.Validate(new AddAgentClaimCommand(null, "key", "value"));
            // Assert
            Assert.Contains("Agent required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("key", "value")]
        [InlineData("role", "Administrator")]
        public void AddAgentClaim_KeyIsValid(string key, string value)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                Email = "user@test.com",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddAgentClaimCommand(agent, key, value));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("", "value")]
        [InlineData(null, "value")]
        public void AddAgentClaim_KeyIsInvalid(string key, string value)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                UserName = "test-user",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddAgentClaimCommand(agent, key, value));
            // Assert
            Assert.Contains("Key required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("key", "value")]
        [InlineData("role", "Administrator")]
        public void AddAgentClaim_ValueIsValid(string key, string value)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                UserName = "test-user",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddAgentClaimCommand(agent, key, value));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("key", "")]
        [InlineData("key", null)]
        public void AddAgentClaim_ValueIsInvalid(string key, string value)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                UserName = "test-user",
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new AddAgentClaimCommand(agent, key, value));
            // Assert
            Assert.Contains("Value required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
