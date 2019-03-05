using NARE.Application.Agent.Query.GetAgentByEmail;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAgentByEmail
{
    public class GetAgentByEmailValidatorTest
    {
        public GetAgentByEmailQueryValidator Validator { get; }

        public GetAgentByEmailValidatorTest()
        {
            // Arrange
            Validator = new GetAgentByEmailQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void GetAgentByEmail_AgentIdIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new GetAgentByEmailQuery(email));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetAgentByEmail_AgentIdIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new GetAgentByEmailQuery(email));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Email is required", result.Errors[0].ErrorMessage);
        }
    }
}
