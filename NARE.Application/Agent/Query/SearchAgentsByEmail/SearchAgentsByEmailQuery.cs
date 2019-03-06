using MediatR;
using NARE.Application.Agent.Model;

namespace NARE.Application.Agent.Query.SearchAgentsByEmail
{
    public class SearchAgentsByEmailQuery : IRequest<PaginatedAgentsDto>
    {
        public SearchAgentsByEmailQuery(string email) => Email = email;

        public string Email { get; set; }
    }
}
