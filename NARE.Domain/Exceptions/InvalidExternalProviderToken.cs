using System;

namespace NARE.Domain.Exceptions
{
    public class InvalidExternalProviderToken : Exception
    {
        public InvalidExternalProviderToken() : base("Invalid access token")
        {
            
        }

        public InvalidExternalProviderToken(string message) : base(message)
        {
            
        }
    }
}
