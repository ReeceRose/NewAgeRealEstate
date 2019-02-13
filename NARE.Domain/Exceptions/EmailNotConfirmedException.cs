using System;

namespace NARE.Domain.Exceptions
{
    public class EmailNotConfirmedException : Exception
    {
        public EmailNotConfirmedException() : base("Email not confirmed")
        {
            
        }

        public EmailNotConfirmedException(string message) : base(message)
        {
            
        }
    }
}
