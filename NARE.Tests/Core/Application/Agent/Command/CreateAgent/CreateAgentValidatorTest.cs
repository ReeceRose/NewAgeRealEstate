using System.Linq;
using NARE.Application.Agent.Command.CreateAgent;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.CreateAgent
{
    public class CreateAgentValidatorTest
    {
        public CreateAgentCommandValidator Validator { get; }

        public CreateAgentValidatorTest()
        {
            // Arrange
            Validator = new CreateAgentCommandValidator();
        }

        [Theory]
        [InlineData("test-user", "Test1!")]
        [InlineData("user@domain.com", "Password1!")]
        public void CreateAgent_AgentIsValid(string agentName, string password)
        {
            // Arrange
            var agent = new AgentDto()
            {
                UserName = agentName,
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new CreateAgentCommand(agent, password));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateAgent_AgentIsInvalid()
        {
            // Act
            var result = Validator.Validate(new CreateAgentCommand(null, "Test1!"));
            // Assert
            Assert.Contains("Agent required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("Test1!")]
        [InlineData("Password!1")]
        public void CreateAgent_PasswordIsValid(string password)
        {
            // Arrange
            var agent = new AgentDto()
            {
                Email = "test@test.com"
            };
            // Act
            var result = Validator.Validate(new CreateAgentCommand(agent, password));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CreateAgent_PasswordIsInvalid(string password)
        {
            // Arrange
            var agent = new AgentDto()
            {
                Email = "test@test.com"
            };
            // Act
            var result = Validator.Validate(new CreateAgentCommand(agent, password));
            // Assert
            Assert.Contains("Password required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
