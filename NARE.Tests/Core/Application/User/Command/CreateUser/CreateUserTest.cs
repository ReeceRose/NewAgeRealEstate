using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using NARE.Application.User.Command.CreateUser;
using NARE.Application.User.Model;
using NARE.Application.Utilities;
using NARE.Domain.Entities;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.User.Command.CreateUser
{
    public class CreateUserTest
    {
        public Mock<MockUserManager> UserManager { get; }
        public IMapper Mapper { get; }
        public CreateUserCommandHandler Handler { get; }

        public CreateUserTest()
        {
            // Arrange
            UserManager = new Mock<MockUserManager>();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            Handler = new CreateUserCommandHandler(UserManager.Object, Mapper);
        }

        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task CreateUser_UserCreated(string email, string password)
        {
            // Arrange
            var user = new ApplicationUserDto()
            {
                Email = email
            };
            UserManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await Handler.Handle(new CreateUserCommand(user, password), CancellationToken.None);
            // Assert
            Assert.True(result.Succeeded);
        }
        [Theory]
        [InlineData("test@test.ca", "Test123!")]
        [InlineData("user@domain.com", "Password!1f4")]
        public async Task CreateUser_UserNotCreated(string email, string password)
        {
            // Arrange
            var user = new ApplicationUserDto()
            {
                Email = email
            };
            UserManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            // Act
            var result = await Handler.Handle(new CreateUserCommand(user, password), CancellationToken.None);
            // Assert
            Assert.False(result.Succeeded);
        }
    }
}
