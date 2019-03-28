using NARE.Application.Listing.Command.RemoveListing;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Command.RemoveListing
{
    public class RemoveListingValidatorTest
    {
        public RemoveListingCommandValidator Validator { get; }

        public RemoveListingValidatorTest()
        {
            // Arrange
            Validator = new RemoveListingCommandValidator();
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0123456789")]
        public void GetListingById_ListingIdIsValid(string listingId)
        {
            // Act
            var result = Validator.Validate(new RemoveListingCommand(listingId));
            // Assert
            Assert.True(result.IsValid);
        }
    }
}
