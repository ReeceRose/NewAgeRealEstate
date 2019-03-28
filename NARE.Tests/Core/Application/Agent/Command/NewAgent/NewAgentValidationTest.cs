using System.Linq;
using NARE.Application.Agent.Command.NewAgent;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Command.NewAgent
{
    public class NewAgentValidationTest
    {
        public NewAgentCommandValidator Validator { get; }
        public NewAgentValidationTest()
        {
            // Arrange
            Validator = new NewAgentCommandValidator();
        }

        [Theory]
        [InlineData("Test Agent")]
        [InlineData("Agent")]
        public void NewAgent_NameIsValid(string name)
        {
            // Act
            var result = Validator.Validate(new NewAgentCommand(name: name, email: "test@test.ca", password: "Test1!"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NewAgent_NameIsInvalid(string name)
        {
            // Act
            var result = Validator.Validate(new NewAgentCommand(name: name, email: "test@test.ca", password: "Test1!"));
            // Assert
            Assert.Contains("Name is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void NewAgent_EmailIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new NewAgentCommand(name: "Test Agent", email: email, password: "Test1!"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test.ca")]
        public void NewAgent_EmailIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new NewAgentCommand(name: "Test Agent", email: email, password: "Test1!"));
            // Assert
            Assert.Contains("Email is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("Test123!")]
        [InlineData("Password!1f4")]
        public void NewAgent_PasswordIsValid(string password)
        {
            // Act
            var result = Validator.Validate(new NewAgentCommand(name: "Test Agent", email: "test@test.ca", password: password));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")] // Empty
        [InlineData(null)] // Null
        [InlineData("T1!")] // Too few characters
        [InlineData("Test12")] // Does not contain any special characters
        [InlineData("Test###")] // Does not contain any numbers
        public void NewAgent_PasswordIsInvalid(string password)
        {
            // Act
            var result = Validator.Validate(new NewAgentCommand(name: "Test Agent", email: "test@test.ca", password: password));
            // Assert
            Assert.Contains("Password does not meet security constraints", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
