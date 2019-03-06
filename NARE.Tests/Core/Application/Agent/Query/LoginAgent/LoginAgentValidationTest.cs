using System.Linq;
using NARE.Application.Agent.Query.LoginAgent;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.LoginAgent
{
    public class LoginAgentValidationTest
    {
        public LoginAgentQueryValidator Validator { get; }
        public LoginAgentValidationTest()
        {
            // Arrange
            Validator = new LoginAgentQueryValidator();
        }

        [Theory]
        [InlineData("test@test.ca")]
        [InlineData("user@domain.com")]
        public void LoginAgent_EmailIsValid(string email)
        {
            // Act
            var result = Validator.Validate(new LoginAgentQuery(email: email, password: "Test1!"));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test.ca")]
        public void LoginAgent_EmailIsInvalid(string email)
        {
            // Act
            var result = Validator.Validate(new LoginAgentQuery(email: email, password: "Test1!"));
            // Assert
            Assert.Contains("Email is required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("Test123!")]
        [InlineData("Password!1f4")]
        public void LoginAgent_PasswordIsValid(string password)
        {
            // Act
            var result = Validator.Validate(new LoginAgentQuery(email: "test@test.ca", password: password));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")] // Empty
        [InlineData(null)] // Null
        [InlineData("T1!")] // Too few characters
        [InlineData("Test12")] // Does not contain any special characters
        [InlineData("Test###")] // Does not contain any numbers
        public void LoginAgent_PasswordIsInvalid(string password)
        {
            // Act
            var result = Validator.Validate(new LoginAgentQuery(email: "test@test.ca", password: password));
            // Assert
            Assert.Contains("Password does not meet security constraints", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
