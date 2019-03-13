using System;

namespace NARE.Domain.Exceptions.Account
{
    public class InvalidCredentialException : Exception
    {
        public InvalidCredentialException() : base("Invalid username or password. Please try again")
        {
            
        }
        public InvalidCredentialException(string message) : base(message)
        {
            
        }
    }
}