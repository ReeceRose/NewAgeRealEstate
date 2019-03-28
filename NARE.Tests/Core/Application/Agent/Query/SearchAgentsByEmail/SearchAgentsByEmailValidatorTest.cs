using NARE.Application.Agent.Query.SearchAgentsByEmail;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.SearchAgentsByEmail
{
    public class SearchAgentsByEmailValidatorTest
    {
        public SearchAgentsByEmailQueryValidator Validator { get; }

        public SearchAgentsByEmailValidatorTest()
        {
            // Arrange
            Validator = new SearchAgentsByEmailQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void SearchAgentssByEmail_AgentIdIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new SearchAgentsByEmailQuery(email));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SearchAgentsByEmail_AgentIdIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new SearchAgentsByEmailQuery(email));
            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Email is required", result.Errors[0].ErrorMessage);
        }
    }
}
