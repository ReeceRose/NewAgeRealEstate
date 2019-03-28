using NARE.Application.Agent.Command.DisableAgent;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.DisableAgent
{
    public class DisableAgentValidatorTest
    {
        public DisableAgentCommandValidator Validator { get; }

        public DisableAgentValidatorTest()
        {
            // Arrange
            Validator = new DisableAgentCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void DisableAgent_AgentIdIsValid(string agentId)
        {
            // Act
            var result = Validator.Validate(new DisableAgentCommand(agentId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void DisableAgent_AgentIdIsInvalid(string agentId)
        {
            // Act
            var result = Validator.Validate(new DisableAgentCommand(agentId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Agent ID required", result.Errors[0].ErrorMessage);
        }
    }
}
