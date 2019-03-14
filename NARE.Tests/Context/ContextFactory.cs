using System;
using Microsoft.EntityFrameworkCore;
using NARE.Domain.Entities;
using NARE.Persistence;

namespace NARE.Tests.Context
{
    public class ContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.Listings.Add(new Listing() { Address = "123 Test", Id = Guid.Parse("def399bb-3d45-478c-9341-98be4e43d128"), Status = ListingStatus.Listed });
            context.Listings.Add(new Listing() { Address = "456 Test", Id = Guid.Parse("c87fa34b-c4d0-4ab7-ab1c-6c283b6a1491"), Status = ListingStatus.Listed });
            context.Users.Add(new Agent() { Email = "test@test.ca", Id = "123", UserName = "test-user" });
            context.SaveChanges();
            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}