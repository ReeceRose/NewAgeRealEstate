using System;

namespace NARE.Domain.Exceptions
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