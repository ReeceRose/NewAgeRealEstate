using NARE.Application.Agent.Query.GetAgentById;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAgentDtoById
{
    public class GetAgentDtoByIdValidatorTest
    {
        public GetAgentByIdQueryValidator Validator { get; }

        public GetAgentDtoByIdValidatorTest()
        {
            // Arrange
            Validator = new GetAgentByIdQueryValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void GetAgentDtoById_AgentIdIsValid(string agentId)
        {
            // Act
            var result = Validator.Validate(new GetAgentByIdQuery(agentId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetAgentDto_AgentIdIsInvalid(string agentId)
        {
            // Act
            var result = Validator.Validate(new GetAgentByIdQuery(agentId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Agent ID required", result.Errors[0].ErrorMessage);
        }
    }
}
