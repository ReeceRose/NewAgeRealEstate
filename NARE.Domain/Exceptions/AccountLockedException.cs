using System;

namespace NARE.Domain.Exceptions
{
    public class AccountLockedException : Exception
    {
        public AccountLockedException() : base("Your account is locked")
        {
            
        }
        
        public AccountLockedException(string message) : base(message)
        {
            
        }
    }
}