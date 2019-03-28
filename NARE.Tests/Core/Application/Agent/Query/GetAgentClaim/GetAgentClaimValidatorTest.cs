using System.Linq;
using NARE.Application.Agent.Query.GetAgentClaim;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GetAgentClaim
{
    public class GetAgentClaimValidatorTest
    {
        public GetAgentClaimQueryValidator Validator { get; }

        public GetAgentClaimValidatorTest()
        {
            // Arrange
            Validator = new GetAgentClaimQueryValidator();
        }

        [Theory]
        [InlineData("test-user")]
        [InlineData("user@domain.com")]
        public void GetAgentClaim_AgentIsValid(string userName)
        {
            // Arrange
            var agent = new AgentDto()
            {
                UserName = userName,
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new GetAgentClaimQuery(agent));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetAgentClaim_AgentIsInvalid()
        {
            // Act
            var result = Validator.Validate(new GetAgentClaimQuery(null));
            // Assert
            Assert.Contains("Agent required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
