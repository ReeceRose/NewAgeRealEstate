using NARE.Application.Agent.Command.UpdateAgent;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.UpdateAgent
{
    public class UpdateAgentValidatorTest
    {
        public UpdateAgentCommandValidator Validator { get; }

        public UpdateAgentValidatorTest()
        {
            // Arrange
            Validator = new UpdateAgentCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void UpdateAgent_AgentIdIsValid(string agentId)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                Id = agentId
            };
            // Act
            var result = Validator.Validate(new UpdateAgentCommand(agent));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void UpdateAgent_AgentIdIsInvalid()
        {
            // Act
            var result = Validator.Validate(new UpdateAgentCommand(null));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Agent required", result.Errors[0].ErrorMessage);
        }
    }
}
