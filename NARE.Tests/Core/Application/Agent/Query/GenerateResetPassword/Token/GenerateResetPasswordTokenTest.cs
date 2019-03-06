using System.Threading;
using Moq;
using NARE.Application.Agent.Query.GenerateResetPassword.Token;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.Agent.Query.GenerateResetPassword.Token
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
            var requestedAgent = new NARE.Domain.Entities.Agent()
            {
                Email = email
            };
            UserManager.Setup(u => u.GeneratePasswordResetTokenAsync(It.IsAny<NARE.Domain.Entities.Agent>())).ReturnsAsync(token);
            // Act
            var returnedToken = Handler.Handle(new GenerateResetPasswordTokenQuery(requestedAgent), CancellationToken.None).Result;
            // Assert
            Assert.Equal(returnedToken, token);
        }
    }
}