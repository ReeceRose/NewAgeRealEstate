using System;

namespace NARE.Domain.Exceptions
{
    public class EmailIsAlreadyConfirmedException : Exception
    {
        public EmailIsAlreadyConfirmedException() : base("Email already confirmed")
        {

        }

        public EmailIsAlreadyConfirmedException(string message) : base(message)
        {

        }
    }
}
