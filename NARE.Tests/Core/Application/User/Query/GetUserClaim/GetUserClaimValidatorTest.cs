using System.Linq;
using NARE.Application.User.Model;
using NARE.Application.User.Query.GetUserClaim;
using Xunit;

namespace NARE.Tests.Core.Application.User.Query.GetUserClaim
{
    public class GetUserClaimValidatorTest
    {
        public GetUserClaimQueryValidator Validator { get; }

        public GetUserClaimValidatorTest()
        {
            // Arrange
            Validator = new GetUserClaimQueryValidator();
        }

        [Theory]
        [InlineData("test-user")]
        [InlineData("user@domain.com")]
        public void GetUserClaim_UserIsValid(string userName)
        {
            // Arrange
            var user = new ApplicationUserDto()
            {
                UserName = userName,
                Id = "123"
            };
            // Act
            var result = Validator.Validate(new GetUserClaimQuery(user));
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetUserClaim_UserIsInvalid()
        {
            // Act
            var result = Validator.Validate(new GetUserClaimQuery(null));
            // Assert
            Assert.Contains("User required", result.Errors.First().ErrorMessage);
            Assert.False(result.IsValid);
        }
    }
}
