using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using AutoMapper;
using Moq;
using NARE.Application.User.Model;
using NARE.Application.User.Query.GetUserClaim;
using NARE.Application.Utilities;
using NARE.Domain.Entities;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.User.Query.GetUserClaim
{
    public class GetUserClaimTest
    {
        public Mock<MockUserManager> UserManager { get; }
        public IMapper Mapper { get; }
        public GetUserClaimQueryHandler Handler { get; }

        public GetUserClaimTest()
        {
            // Arrange
            UserManager = new Mock<MockUserManager>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Handler = new GetUserClaimQueryHandler(UserManager.Object, Mapper);
        }

        [Theory]
        [InlineData("key", "value")]
        [InlineData("role", "Admin")]
        public void GetUserClaim_ReturnsAClaim(string key, string value)
        {
            // Arrange
            var user = new ApplicationUserDto()
            {
                Email = "user@test.com"
            };
            var claims = new List<Claim>()
            {
                new Claim(key, value)
            };
            UserManager.Setup(u => u.GetClaimsAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(claims);
            // Act
            var returnedClaims = Handler.Handle(new GetUserClaimQuery(user), CancellationToken.None).Result;
            // Assert
            Assert.Single(returnedClaims);
        }

        [Theory]
        [InlineData("key", "value")]
        [InlineData("role", "Admin")]
        public void GetUserClaim_ReturnsClaims(string key, string value)
        {
            // Arrange
            var user = new ApplicationUserDto()
            {
                Email = "user@test.com"
            };
            var claims = new List<Claim>()
            {
                new Claim(key, value),
                new Claim(key, value),
                new Claim(key, value)
            };
            UserManager.Setup(u => u.GetClaimsAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(claims);
            // Act
            var returnedClaims = Handler.Handle(new GetUserClaimQuery(user), CancellationToken.None).Result;
            // Assert
            Assert.Equal(3, returnedClaims.Count);
        }

        [Fact]
        public void GetUserClaim_ReturnsEmpty()
        {
            // Arrange
            var user = new ApplicationUserDto()
            {
                Email = "user@test.com"
            };
            var claims = new List<Claim>();

            UserManager.Setup(u => u.GetClaimsAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(claims);
            // Act
            var returnedClaims = Handler.Handle(new GetUserClaimQuery(user), CancellationToken.None).Result;
            // Assert
            Assert.Empty(returnedClaims);
        }
    }
}
