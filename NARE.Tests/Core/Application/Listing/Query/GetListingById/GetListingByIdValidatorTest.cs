using System;
using NARE.Application.Listing.Query.GetListingById;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetListingById
{
    public class GetListingByIdValidatorTest
    {
        public GetListingByIdQueryValidator Validator { get; }

        public GetListingByIdValidatorTest()
        {
            // Arrange
            Validator = new GetListingByIdQueryValidator();
        }

        [Fact]
        public void GetListingById_ListingIdIsValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            // Act
            var result = Validator.Validate(new GetListingByIdQuery(id));
            // Assert
            Assert.True(result.IsValid);
        }
    }
}