using NARE.Application.Listing.Query.GetAllListingsPaginated;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetAllListingsPaginated
{
    public class GetAllListingsPaginatedValidatorTest
    {
        public GetAllListingsPaginatedQueryValidator Validator { get; }

        public GetAllListingsPaginatedValidatorTest()
        {
            // Arrange
            Validator = new GetAllListingsPaginatedQueryValidator();
        }

        [Fact]
        public void GetAllListingsPaginated_PaginationModelIsValid()
        {
            // Act
            var result = Validator.Validate(new GetAllListingsPaginatedQuery(new PaginationModel()));
            // Assert
            Assert.True(result.IsValid);
        }
    }
}
