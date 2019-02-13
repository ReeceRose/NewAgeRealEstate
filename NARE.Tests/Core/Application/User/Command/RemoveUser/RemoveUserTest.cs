using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using NARE.Application.User.Command.RemoveUser;
using NARE.Application.User.Query.GetUserById;
using NARE.Domain.Entities;
using NARE.Domain.Exceptions;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.User.Command.RemoveUser
{
    public class RemoveUserTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockUserManager> UserManager { get; }
        public RemoveUserCommandHandler Handler { get; }

        public RemoveUserTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            UserManager = new Mock<MockUserManager>();
            Handler = new RemoveUserCommandHandler(Mediator.Object, UserManager.Object);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task RemoveUserTest_ThrowsInvalidUserException(string userId)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync((ApplicationUser) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(new RemoveUserCommand(userId), CancellationToken.None));
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task RemoveUserTest_RemovesSuccessfully(string userId)
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Id = userId
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).ReturnsAsync(user);
            UserManager.Setup(u => u.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await Handler.Handle(new RemoveUserCommand(userId), CancellationToken.None);
            // Assert
            Assert.True(result);
        }
    }
}
