using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NARE.Application.Listing.Model;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Application.Listing.Query.GetAllListingsPaginated;
using NARE.Application.Listing.Query.GetPaginatedListingsResult;
using NARE.Application.Utilities;
using NARE.Domain.Entities;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetAllListingsPaginated
{
    public class GetAllListingsPaginatedTest
    {
        public Mock<IMediator> Mediator { get; }
        public IMapper Mapper { get; }
        public GetAllListingsPaginatedQueryHandler Handler { get; }
        public List<NARE.Domain.Entities.Listing> Listings { get; }

        public GetAllListingsPaginatedTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Handler = new GetAllListingsPaginatedQueryHandler(Mediator.Object, Mapper);
            Listings = new List<NARE.Domain.Entities.Listing>()
            {
                new NARE.Domain.Entities.Listing() { Address = "123 test" },
                new NARE.Domain.Entities.Listing() { Address = "456 test" }
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAllListingsQuery>(), default(CancellationToken))).ReturnsAsync(Listings);
        }

        [Theory]
        [InlineData(3, 1, 1)]
        [InlineData(1, 1, 2)]
        [InlineData(10, 1, 1)]
        public async Task GetAllListingsPaginated_ReturnsExpected(int pageSize, int currentPage, int totalPages)
        {
            // Arrange
            var paginatedListings = new PaginatedListingsDto()
            {
                PaginationModel = new PaginationModel()
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize
                },
                Listings = Listings
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetAllListingsPaginatedQuery>(), default(CancellationToken))).ReturnsAsync(paginatedListings);
            Mediator.Setup(m => m.Send(It.IsAny<GetPaginatedListingsResultQuery>(), default(CancellationToken))).ReturnsAsync(paginatedListings);
            // Act
            var result = await Handler.Handle(new GetAllListingsPaginatedQuery(paginatedListings.PaginationModel), CancellationToken.None);
            // Assert
            Assert.Equal(pageSize, result.PaginationModel.PageSize);
            Assert.Equal(currentPage, result.PaginationModel.CurrentPage);
            Assert.Equal(totalPages, result.PaginationModel.TotalPages);
        }
    }
}
