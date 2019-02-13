using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NARE.Application.User.Query.GenerateEmailConfirmation.Token;
using NARE.Domain.Entities;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.User.Query.GenerateEmailConfirmation.Token
{
    public class GenerateEmailConfirmationTokenTest
    {
        public List<ApplicationUser> Users { get; }
        public Mock<MockUserManager> UserManager { get; }
        public GenerateEmailConfirmationTokenQueryHandler Handler { get; }

        public GenerateEmailConfirmationTokenTest()
        {
            // Arrange
            Users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Email = "test@test.ca",
                    UserName = "test-user",
                    EmailConfirmed = false
                }
            };
            UserManager = new Mock<MockUserManager>();
            Handler = new GenerateEmailConfirmationTokenQueryHandler(UserManager.Object);
        }

        [Fact]
        public void GenerateEmailConfirmationToken_ShouldReturnToken()
        {
            // Arrange
            UserManager.Setup(m => m.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult("1234567890"));
            // Act
            var token = Handler.Handle(new GenerateEmailConfirmationTokenQuery(Users.First()), default(CancellationToken)).Result;
            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
    }
}