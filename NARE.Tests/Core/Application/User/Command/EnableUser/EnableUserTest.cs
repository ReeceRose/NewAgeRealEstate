using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.User.Command.EnableUser;
using NARE.Application.User.Command.UpdateUser;
using NARE.Application.User.Query.GetUserById;
using NARE.Domain.Entities;
using NARE.Domain.Exceptions;
using Xunit;

namespace NARE.Tests.Core.Application.User.Command.EnableUser
{
    public class EnableUserTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<ILogger<EnableUserCommandHandler>> Logger { get; }
        public EnableUserCommandHandler Handler { get; }

        public EnableUserTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            Logger = new Mock<ILogger<EnableUserCommandHandler>>();
            Handler = new EnableUserCommandHandler(Mediator.Object, Logger.Object);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("0987654321")]
        public async Task EnableUser_ThrowsInvalidUserException(string userId)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken)))
                .ReturnsAsync((ApplicationUser) null);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() =>
                Handler.Handle(new EnableUserCommand(userId), CancellationToken.None));
        }

        [Theory]
        [InlineData("1234567890", "user@domain.com")]
        [InlineData("0987654321", "test@test.ca")]
        public async Task EnableUser_ReturnsTrue(string userId, string email)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Id = userId,
                Email = email,
                AccountEnabled = false
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken)))
                .ReturnsAsync(requestedUser);
            Mediator.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), default(CancellationToken))).ReturnsAsync(true);
            // Act
            var result = await Handler.Handle(new EnableUserCommand(userId), CancellationToken.None);
            // Assert
            Assert.True(result);
        }
    }
}