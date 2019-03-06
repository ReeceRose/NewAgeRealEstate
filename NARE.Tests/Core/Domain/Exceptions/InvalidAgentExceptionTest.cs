using NARE.Domain.Exceptions;
using Xunit;

namespace NARE.Tests.Core.Domain.Exceptions
{
    public class InvalidAgentExceptionTest
    {
        [Fact]
        public void InvalidAgentException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new InvalidAgentException();
            // Assert
            Assert.Equal("An agent with this ID or email does not exist", error.Message);
            Assert.Equal(typeof(InvalidAgentException), error.GetType());
        }

        [Theory]
        [InlineData("Unknown agent")]
        [InlineData("Error: Not an agent")]
        public void InvalidAgentException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new InvalidAgentException(errorMessage);
            // Assert
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(InvalidAgentException), error.GetType());
        }
    }
}
