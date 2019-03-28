using System;

namespace NARE.Domain.Exceptions.Listing
{
    public class InvalidListingException : Exception
    {
        public InvalidListingException() : base("Invalid listing ID")
        {

        }

        public InvalidListingException(string message) : base(message)
        {

        }
    }
}
