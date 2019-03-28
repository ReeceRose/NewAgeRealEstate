using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NARE.Application.Listing.Model;
using NARE.Application.Listing.Query.GetAllActiveListings;
using NARE.Application.Listing.Query.GetPaginatedListingsResult;
using NARE.Application.Listing.Query.SearchActiveListings;
using NARE.Application.Utilities;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.SearchActiveListings
{
    public class SearchActiveListingsTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public List<NARE.Domain.Entities.Listing> Listings { get; }
        public SearchActiveListingsQueryHandler Handler { get; }

        public SearchActiveListingsTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Listings = new List<NARE.Domain.Entities.Listing>()
            {
                new NARE.Domain.Entities.Listing() { City = "Toronto" },
                new NARE.Domain.Entities.Listing() { City = "Toronto" },
                new NARE.Domain.Entities.Listing() { City = "Toronto" },
                new NARE.Domain.Entities.Listing() { City = "Oakville" },
                new NARE.Domain.Entities.Listing() { City = "Oakville" },
                new NARE.Domain.Entities.Listing() { City = "Oakville" },
                new NARE.Domain.Entities.Listing() { City = "Montreal" },

            };
            Handler = new SearchActiveListingsQueryHandler(Mediator.Object, Mapper);
        }

        [Theory]
        [InlineData("Toronto", 3)]
        [InlineData("Oakville", 3)]
        [InlineData("Montreal", 1)]
        [InlineData("Brampton", 0)]
        public async Task SearchActiveListings_ReturnsExpectedCount(string city, int expectedCount)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetAllActiveListingsQuery>(), default(CancellationToken))).ReturnsAsync(Listings);
            Mediator.Setup(m => m.Send(It.IsAny<GetPaginatedListingsResultQuery>(), default(CancellationToken)))
                .ReturnsAsync(new PaginatedListingsDto()
                    {Listings = Listings.Where(l => l.City == city).ToList(), PaginationModel = new PaginationModel()});
            // Act
            var result = await Handler.Handle(new SearchActiveListingsQuery(new PaginationModel(), new SearchModel() {City = city }), CancellationToken.None);
            // Assert
            Assert.Equal(result.Listings.Count, expectedCount);
        }
    }
}
