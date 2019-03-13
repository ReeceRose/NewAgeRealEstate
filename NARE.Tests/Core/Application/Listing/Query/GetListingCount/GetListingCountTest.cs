using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Application.Listing.Query.GetListingCount;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetListingCount
{
    public class GetListingCountTest
    {
        public Mock<IMediator> Mediator { get; }
        public GetListingCountQueryHandler Handler { get; }

        public GetListingCountTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Handler = new GetListingCountQueryHandler(Mediator.Object);
        }

        [Fact]
        public async Task GetListingCount_ReturnsZero()
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAllListingsQuery>(), default(CancellationToken))).ReturnsAsync((List<NARE.Domain.Entities.Listing>)null);
            // Act
            var result = await Handler.Handle(new GetListingCountQuery(), CancellationToken.None);
            // Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(10000)]
        public async Task GetListingCount_ReturnsValidCount(int listingCount)
        {
            // Arrange
            var listings = new List<NARE.Domain.Entities.Listing>();
            for (var i = 0; i < listingCount; i++)
            {
                listings.Add(new NARE.Domain.Entities.Listing());
            }

            Mediator.Setup(m => m.Send(It.IsAny<GetAllListingsQuery>(), default(CancellationToken))).ReturnsAsync(listings);
            // Act
            var result = await Handler.Handle(new GetListingCountQuery(), CancellationToken.None);
            // Assert
            Assert.Equal(result, listings.Count);
        }
    }
}
