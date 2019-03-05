using NARE.Application.Agent.Command.RemoveAgent;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.RemoveAgent
{
    public class RemoveAgentValidatorTest
    {
        public RemoveAgentCommandValidator Validator { get; }

        public RemoveAgentValidatorTest()
        {
            // Arrange
            Validator = new RemoveAgentCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void RemoveAgent_AgentIdIsValid(string agentId)
        {
            // Act
            var result = Validator.Validate(new RemoveAgentCommand(agentId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void RemoveAgent_AgentIdIsInvalid(string agentId)
        {
            // Act
            var result = Validator.Validate(new RemoveAgentCommand(agentId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Agent ID required", result.Errors[0].ErrorMessage);
        }
    }
}
