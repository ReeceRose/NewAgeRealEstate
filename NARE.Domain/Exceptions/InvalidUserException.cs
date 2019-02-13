using System;

namespace NARE.Domain.Exceptions
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException() : base("A user with this ID or email does not exist")
        {
            
        }

        public InvalidUserException(string message) : base(message)
        {
            
        }
    }
}
