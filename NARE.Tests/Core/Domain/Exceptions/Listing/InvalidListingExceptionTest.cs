using NARE.Domain.Exceptions.Listing;
using Xunit;

namespace NARE.Tests.Core.Domain.Exceptions.Listing
{
    public class InvalidListingExceptionTest
    {
        [Fact]
        public void InvalidListingException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new InvalidListingException();
            // Assert
            Assert.Equal("Invalid listing ID", error.Message);
            Assert.Equal(typeof(InvalidListingException), error.GetType());
        }

        [Theory]
        [InlineData("Unknown listing")]
        [InlineData("Error: Not a valid ID")]
        public void InvalidListingException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new InvalidListingException(errorMessage);
            // Assert
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(InvalidListingException), error.GetType());
        }
    }
}
