using NARE.Application.Listing.Query.GetAllActiveListingsPaginated;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetAllActiveListingsPaginated
{
    public class GetAllActiveListingsPaginatedValidatorTest
    {
        public GetAllActiveListingsPaginatedQueryValidator Validator { get; }

        public GetAllActiveListingsPaginatedValidatorTest()
        {
            // Arrange
            Validator = new GetAllActiveListingsPaginatedQueryValidator();
        }

        [Fact]
        public void GetAllActiveListingsPaginated_PaginationModelIsValid()
        {
            // Act
            var result = Validator.Validate(new GetAllActiveListingsPaginatedQuery(new PaginationModel()));
            // Assert
            Assert.True(result.IsValid);
        }
    }
}
