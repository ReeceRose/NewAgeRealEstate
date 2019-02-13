using System.Threading;
using Moq;
using NARE.Application.User.Query.GenerateResetPassword.Token;
using NARE.Domain.Entities;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.User.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenTest
    {
        public Mock<MockUserManager> UserManager { get; }
        public GenerateResetPasswordTokenQueryHandler Handler { get; }

        public GenerateResetPasswordTokenTest()
        {
            // Arrange
            UserManager = new Mock<MockUserManager>();
            Handler = new GenerateResetPasswordTokenQueryHandler(UserManager.Object);
        }

        [Theory]
        [InlineData("test@test.ca", "1234567890")]
        [InlineData("user@domain.com", "9876543210")]
        public void GenerateResetPasswordToken_ReturnsValidToken(string email, string token)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Email = email
            };
            UserManager.Setup(u => u.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(token);
            // Act
            var returnedToken = Handler.Handle(new GenerateResetPasswordTokenQuery(requestedUser), CancellationToken.None).Result;
            // Assert
            Assert.Equal(returnedToken, token);
        }
    }
}