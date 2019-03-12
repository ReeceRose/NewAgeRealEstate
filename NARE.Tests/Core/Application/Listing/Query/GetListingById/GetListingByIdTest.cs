using System;
using System.Threading;
using System.Threading.Tasks;
using NARE.Application.Listing.Query.GetListingById;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetListingById
{
    public class GetListingByIdTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public GetListingByIdQueryHandler Handler { get; }

        public GetListingByIdTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new GetListingByIdQueryHandler(Context);
        }

        [Theory]
        [InlineData("def399bb-3d45-478c-9341-98be4e43d128", "123 Test")]
        [InlineData("c87fa34b-c4d0-4ab7-ab1c-6c283b6a1491", "456 Test")]
        public async Task GetListingById_ReturnsExpectedListing(string id, string address)
        {
            // Act
            var listing = await Handler.Handle(new GetListingByIdQuery(Guid.Parse(id)), CancellationToken.None);
            // Assert
            Assert.Equal(Guid.Parse(id), listing.Id);
            Assert.Equal(address, listing.Address);
        }

        [Theory]
        [InlineData("19af91ac-f342-48a6-9822-efc7571c0fe9")]
        [InlineData("201c9fa5-e902-4d6c-884b-685d29f8ffab")]
        public async Task GetListingById_ReturnsNullListing(string id)
        {
            // Act
            var listing = await Handler.Handle(new GetListingByIdQuery(Guid.Parse(id)), CancellationToken.None);
            // Assert
            Assert.Null(listing);
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }
}
