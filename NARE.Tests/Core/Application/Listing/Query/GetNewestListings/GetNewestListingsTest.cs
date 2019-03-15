using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Application.Listing.Query.GetNewestListings;
using NARE.Application.Utilities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetNewestListings
{
    public class GetNewestListingsTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public List<NARE.Domain.Entities.Listing> Listings { get; }
        public GetNewestListingsQueryHandler Handler { get; }

        public GetNewestListingsTest()
        {
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Listings = new List<NARE.Domain.Entities.Listing>()
            {
                new NARE.Domain.Entities.Listing() { Address = "123 test", ListingDate = DateTime.Now },
                new NARE.Domain.Entities.Listing() { Address = "456 test", ListingDate = DateTime.Now.AddDays(-1) },
                new NARE.Domain.Entities.Listing() { Address = "789 test",  ListingDate = DateTime.Now.AddDays(-2) },
                new NARE.Domain.Entities.Listing() { Address = "714 test",  ListingDate = DateTime.Now.AddDays(-3) },
                new NARE.Domain.Entities.Listing() { Address = "967 test",  ListingDate = DateTime.Now.AddDays(-4) },
            };
            Handler = new GetNewestListingsQueryHandler(Mediator.Object, Mapper);
            Mediator.Setup(m => m.Send(It.IsAny<GetAllListingsQuery>(), default(CancellationToken))).ReturnsAsync(Listings);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetNewestListings_ReturnsExpectedCount(int count)
        {
            // Act
            var result = await Handler.Handle(new GetNewestListingsQuery(count), CancellationToken.None);
            // Assert
            Assert.Equal(result.Count, count);
        }
        [Theory]
        [InlineData(6)]
        [InlineData(9)]
        [InlineData(12)]
        [InlineData(100)]
        public async Task GetNewestListings_ReturnsNotExpectedCount(int count)
        {
            // Act
            var result = await Handler.Handle(new GetNewestListingsQuery(count), CancellationToken.None);
            // Assert
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetNewestListings_ReturnsExpectedDates()
        {
            // Act
            var result = await Handler.Handle(new GetNewestListingsQuery(3), CancellationToken.None);
            // Assert
            Assert.Equal(DateTime.Now.Date, result.First().ListingDate.Date);
            Assert.Equal(DateTime.Now.AddDays(-1).Date, result.Skip(1).First().ListingDate.Date);
            Assert.Equal(DateTime.Now.AddDays(-2).Date, result.Skip(2).First().ListingDate.Date);
        }
    }
}
