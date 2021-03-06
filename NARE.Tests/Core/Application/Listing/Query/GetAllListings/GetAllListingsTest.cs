﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Persistence;
using NARE.Tests.Context;
using Xunit;

namespace NARE.Tests.Core.Application.Listing.Query.GetAllListings
{
    public class GetAllListingsTest : IDisposable
    {
        public ApplicationDbContext Context { get; }
        public GetAllListingsQueryHandler Handler { get; }

        public GetAllListingsTest()
        {
            // Arrange
            Context = ContextFactory.Create();
            Handler = new GetAllListingsQueryHandler(Context);
        }

        [Fact]
        public async Task GetAllListings_ReturnsZero()
        {
            // Arrange
            // Context by default has two listings, so remove them
            Context.Remove(Context.Listings.First());
            Context.Remove(Context.Listings.Last());
            Context.SaveChanges();
            // Act
            var result = await Handler.Handle(new GetAllListingsQuery(), CancellationToken.None);
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllListings_ReturnsValidAgentCount()
        {
            // Arrange / Act
            var result = await Handler.Handle(new GetAllListingsQuery(), CancellationToken.None);
            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }
}
