using System;

namespace NARE.Domain.Exceptions.Account
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