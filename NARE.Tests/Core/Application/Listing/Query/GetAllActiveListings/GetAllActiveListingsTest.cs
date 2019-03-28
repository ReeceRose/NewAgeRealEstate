using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NARE.Application.Listing.Query.GetAllActiveListings;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetAllActiveListings
{
    public class GetAllActiveListingsTest
    {
        public Mock<IMediator> Mediator { get; }
        public GetAllActiveListingsQueryHandler Handler { get; }

        public List<NARE.Domain.Entities.Listing> Listings { get; }

        public GetAllActiveListingsTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Handler = new GetAllActiveListingsQueryHandler(Mediator.Object);
            Listings = new List<NARE.Domain.Entities.Listing>()
            {
                new NARE.Domain.Entities.Listing() { Address = "123 test", Status = ListingStatus.Listed},
                new NARE.Domain.Entities.Listing() { Address = "456 test", Status = ListingStatus.Sold}
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAllListingsQuery>(), default(CancellationToken))).ReturnsAsync(Listings);
        }

        [Fact]
        public async Task GetAllActiveListings_ReturnsExpectedListing()
        {
            // Act
            var result = await Handler.Handle(new GetAllActiveListingsQuery(), CancellationToken.None);
            // Assert
            Assert.Single(result);
        }
    }
}
