using System;

namespace NARE.Domain.Exceptions
{
    public class AccountAlreadyExistsException : Exception
    {
        public AccountAlreadyExistsException() : base("Account with this email already exists")
        {
            
        }
        
        public AccountAlreadyExistsException(string message) : base(message)
        {
            
        }
    }
}