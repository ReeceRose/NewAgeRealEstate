using NARE.Application.Listing.Query.GetAgentListings;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetAgentListings
{
    public class GetAgentListingsValidatorTest
    {
        public GetAgentListingsQueryValidator Validator { get; }

        public GetAgentListingsValidatorTest()
        {
            // Arrange
            Validator = new GetAgentListingsQueryValidator();
        }

        [Fact]
        public void GetAgentListingsPaginated_PaginationModelIsValid()
        {
            // Act
            var result = Validator.Validate(new GetAgentListingsQuery("123", new PaginationModel()));
            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public void GetAgentListingsPaginated_AgentIdIsValid(string agentId)
        {
            // Act
            var result = Validator.Validate(new GetAgentListingsQuery(agentId, new PaginationModel()));
            // Assert
            Assert.True(result.IsValid);
        }
    }
}
