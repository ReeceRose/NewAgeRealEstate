using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Application.Listing.Query.GetFeaturedListings;
using NARE.Application.Utilities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetFeaturedListings
{
    public class GetFeaturedListingsTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; set; }
        public List<NARE.Domain.Entities.Listing> Listings { get; }
        public GetFeaturedListingsQueryHandler Handler { get; }

        public GetFeaturedListingsTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Listings = new List<NARE.Domain.Entities.Listing>()
            {
                new NARE.Domain.Entities.Listing() { Address = "123 test", Featured = true },
                new NARE.Domain.Entities.Listing() { Address = "456 test", Featured = false },
                new NARE.Domain.Entities.Listing() { Address = "789 test", Featured = false },
                new NARE.Domain.Entities.Listing() { Address = "714 test", Featured = true },
                new NARE.Domain.Entities.Listing() { Address = "967 test", Featured = true },
            };
            Handler = new GetFeaturedListingsQueryHandler(Mediator.Object, Mapper);
            Mediator.Setup(m => m.Send(It.IsAny<GetAllListingsQuery>(), default(CancellationToken))).ReturnsAsync(Listings);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFeaturedListings_ReturnsExpectedCount(int count)
        {
            // Act
            var result = await Handler.Handle(new GetFeaturedListingsQuery(count), CancellationToken.None);
            // Assert
            Assert.Equal(result.Count, count);
        }
        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task GetFeaturedListings_ReturnsNotExpectedCount(int count)
        {
            // Act
            var result = await Handler.Handle(new GetFeaturedListingsQuery(count), CancellationToken.None);
            // Assert
            Assert.Equal(3, result.Count);
        }
    }
}
