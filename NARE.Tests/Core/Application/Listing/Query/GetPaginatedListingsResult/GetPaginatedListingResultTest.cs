using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NARE.Application.Listing.Query.GetPaginatedListingsResult;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetPaginatedListingsResult
{
    public class GetPaginatedListingResultTest
    {
        public List<NARE.Domain.Entities.Listing> Listings { get; }
        public GetPaginatedListingsResultQueryHandler Handler { get; }

        public GetPaginatedListingResultTest()
        {
            // Arrange
            Listings = new List<NARE.Domain.Entities.Listing>()
            {
                new NARE.Domain.Entities.Listing() { Address = "123 test" },
                new NARE.Domain.Entities.Listing() { Address = "456 test" }
            };
            Handler = new GetPaginatedListingsResultQueryHandler();
        }

        [Theory]
        // Based off of the agent list above
        [InlineData(3, 1)]
        [InlineData(1, 1)]
        [InlineData(10, 1)]
        public async Task GetPaginatedListingsResult_ReturnsExpected(int pageSize, int currentPage)
        {
            // Arrange
            var model = new PaginationModel()
            {
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            // Act
            var result = await Handler.Handle(new GetPaginatedListingsResultQuery(Listings, model), CancellationToken.None);
            // Assert
            Assert.Equal(2, result.Listings.Count);
            Assert.Equal(pageSize, result.PaginationModel.PageSize);
            Assert.Equal(currentPage, result.PaginationModel.CurrentPage);
        }
    }
}
