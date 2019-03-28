using NARE.Application.Agent.Query.GetAgentDtoById;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAgentById
{
    public class GetAgentByIdValidatorTest
    {
        public GetAgentByIdQueryValidator Validator { get; }

        public GetAgentByIdValidatorTest()
        {
            // Arrange
            Validator = new GetAgentByIdQueryValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void GetAgentById_AgentIdIsValid(string agentId)
        {
            // Act
            var result = Validator.Validate(new GetAgentDtoByIdQuery(agentId));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetAgentById_AgentIdIsInvalid(string agentId)
        {
            // Act
            var result = Validator.Validate(new GetAgentDtoByIdQuery(agentId));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Agent ID required", result.Errors[0].ErrorMessage);
        }
    }
}