using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NARE.Application.Listing.Command.UpdateListing;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Command.UpdateListing
{
    public class UpdateListingTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public UpdateListingCommandHandler Handler { get; }

        public UpdateListingTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new UpdateListingCommandHandler(Context);
        }

        [Theory]
        [InlineData("1 Test Method")]
        public async Task UpdateListing_UpdatesListing(string address)
        {
            // Arrange
            var listing = Context.Listings.First();
            listing.Address = address;
            listing.ListingStatus = "Listed";
            // Act
            await Handler.Handle(new UpdateListingCommand(listing), CancellationToken.None);
            // Assert
            Assert.Equal(address, Context.Listings.First().Address);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
