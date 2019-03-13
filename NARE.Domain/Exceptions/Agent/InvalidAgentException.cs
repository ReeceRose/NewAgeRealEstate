using System;

namespace NARE.Domain.Exceptions.Agent
{
    public class InvalidAgentException : Exception
    {
        public InvalidAgentException() : base("An agent with this ID or email does not exist")
        {
            
        }

        public InvalidAgentException(string message) : base(message)
        {
            
        }
    }
}
