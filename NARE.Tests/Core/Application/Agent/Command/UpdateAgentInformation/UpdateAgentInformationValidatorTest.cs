using NARE.Application.Agent.Command.UpdateAgentInformation;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.UpdateAgentInformation
{
    public class UpdateAgentInformationValidatorTest
    {
        public UpdateAgentInformationCommandValidator Validator { get; }

        public UpdateAgentInformationValidatorTest()
        {
            // Arrange
            Validator = new UpdateAgentInformationCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void UpdateAgentInformation_AgentIsValid(string agentId)
        {
            // Arrange
            var agent = new NARE.Domain.Entities.Agent()
            {
                Id = agentId
            };
            // Act
            var result = Validator.Validate(new UpdateAgentInformationCommand(agent));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void UpdateAgentInformation_AgentIsInvalid()
        {
            // Act
            var result = Validator.Validate(new UpdateAgentInformationCommand(null));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Agent required", result.Errors[0].ErrorMessage);
        }
    }
}
