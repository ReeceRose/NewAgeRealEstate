﻿using NARE.Domain.Exceptions.Account;
using Xunit;

namespace NARE.Tests.Core.Domain.Exceptions.Account
{
    public class InvalidCredentialExceptionTest
    {
        [Fact]
        public void InvalidCredentialException_ReturnsDefaultErrorMessage()
        {
            // Act
            var error = new InvalidCredentialException();
            // Assert
            Assert.Equal("Invalid username or password. Please try again", error.Message);
            Assert.Equal(typeof(InvalidCredentialException), error.GetType());
        }

        [Theory]
        [InlineData("Invalid login attempt")]
        [InlineData("Error: No agent with these credentials found")]
        public void InvalidCredentialException_ReturnsProvidedErrorMessage(string errorMessage)
        {
            // Act
            var error = new InvalidCredentialException(errorMessage);
            // Assert
            Assert.Equal(errorMessage, error.Message);
            Assert.Equal(typeof(InvalidCredentialException), error.GetType());
        }
    }
}
