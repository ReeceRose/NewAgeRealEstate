using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NARE.Application.User.Command.ConfirmUserEmail;
using NARE.Application.User.Query.GetUserById;
using NARE.Domain.Entities;
using NARE.Domain.Exceptions;
using NARE.Tests.Helpers;
using Xunit;

namespace NARE.Tests.Core.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailTest
    {
        public Mock<IMediator> Mediator { get; }
        public Mock<MockUserManager> UserManager { get; }
        public Mock<ILogger<ConfirmUserEmailCommandHandler>> Logger { get; }
        public ConfirmUserEmailCommandHandler Handler { get; }

        public ConfirmUserEmailTest()
        {
            // Arrange
            Mediator = new Mock<IMediator>();
            UserManager = new Mock<MockUserManager>();
            Logger = new Mock<ILogger<ConfirmUserEmailCommandHandler>>();
            Handler = new ConfirmUserEmailCommandHandler(Mediator.Object, UserManager.Object, Logger.Object);
        }

        [Theory]
        [InlineData("1234234", "0954368")]
        [InlineData("0954368", "1234234")]
        public void ConfirmUserEmail_SuccessfullyConfirmsEmail(string userId, string token)
        {
            // Arrange
            var requestedUser = new ApplicationUser()
            {
                Id =  userId
            };
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).Returns(Task.FromResult(requestedUser));
            UserManager.Setup(u => u.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            var query = new ConfirmUserEmailCommand(userId, token);
            // Act
            var result = Handler.Handle(query, CancellationToken.None).Result;
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("1234234", "0954368")]
        [InlineData("0954368", "1234234")]
        public async Task ConfirmUser_ThrowsInvalidUserException(string userId, string token)
        {
            // Arrange
            Mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default(CancellationToken))).Returns(Task.FromResult((ApplicationUser) null));
            var query = new ConfirmUserEmailCommand(userId, token);
            // Act / Assert
            await Assert.ThrowsAsync<InvalidUserException>(() => Handler.Handle(query, CancellationToken.None));
        }
    }
}
