using NARE.Application.Agent.Command.EnableAgent;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.EnableAgent
{
    public class EnableAgentValidatorTest
    {
        public EnableAgentCommandValidator Validator { get; }

        public EnableAgentValidatorTest()
        {
            // Arrange
            Validator = new EnableAgentCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void EnableAgent_AgentIdIsValid(string agentId)
        {
            // Act
            var result = Validator.Validate(new EnableAgentCommand(agentId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Enable_AgentIdIsInvalid(string agentId)
        {
            // Act
            var result = Validator.Validate(new EnableAgentCommand(agentId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Agent ID required", result.Errors[0].ErrorMessage);
        }
    }
}
