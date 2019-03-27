using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Listing.Command.CreateListing;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Command.CreateListing
{
    public class CreateListingTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public Mock<ILogger<CreateListingCommandHandler>> Logger { get; }
        public CreateListingCommandHandler Handler { get; }

        public CreateListingTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Logger = new Mock<ILogger<CreateListingCommandHandler>>();
            Handler = new CreateListingCommandHandler(Context, Logger.Object);
        }

        [Theory]
        [InlineData("7a8b0360-a637-434d-aa42-f82bfa8afd81", "789 Test", "Test description")]
        [InlineData("7a8b0360-a637-434d-aa42-f82bfa8afd93", "167 Test", "Description")]
        public async Task CreateListing_AddsNewListing(string id, string address, string description)
        {
            // Arrange
            var listingId = Guid.Parse(id);
            var listing = new NARE.Domain.Entities.Listing() { Address = address, Id = listingId, Description = description, Agent = new NARE.Domain.Entities.Agent() { Id = "123" }};
            // Act
            await Handler.Handle(new CreateListingCommand(listing), CancellationToken.None);
            // Assert
            Assert.True(Context.Listings.Contains(listing));
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
