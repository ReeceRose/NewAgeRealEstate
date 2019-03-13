using System;

namespace NARE.Application.Agent.Model
{
    public class AgentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool AccountEnabled { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
