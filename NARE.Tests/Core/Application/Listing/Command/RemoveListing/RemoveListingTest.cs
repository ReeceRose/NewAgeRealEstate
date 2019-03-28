using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.Listing.Command.RemoveListing;
using NARE.Domain.Exceptions.Listing;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Command.RemoveListing
{
    public class RemoveListingTest : IDisposable
    {
        public Mock<IMediator> Mediator { get; }
        public ApplicationDbContext Context { get; }
        public Mock<ILogger<RemoveListingCommandHandler>> Logger { get; }
        public RemoveListingCommandHandler Handler { get; }

        public RemoveListingTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Context = ContextFactory.Create();
            Logger = new Mock<ILogger<RemoveListingCommandHandler>>();
            Handler = new RemoveListingCommandHandler(Mediator.Object, Context, Logger.Object);
        }

        [Theory]
        [InlineData("def399bb-3d45-478c-9341-98be4e43d121")]
        [InlineData("a87fa34b-c4d0-4ab7-ab1c-6c283b6a1491")]
        public async Task RemoveListingTest_ThrowsInvalidListingException(string listingId)
        {
            // Act / Assert
            await Assert.ThrowsAsync<InvalidListingException>(() => Handler.Handle(new RemoveListingCommand(listingId), CancellationToken.None));

        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
