using MediatR;

namespace NARE.Application.Agent.Query.GetAgentByEmail
{
    public class GetAgentByEmailQuery : IRequest<Domain.Entities.Agent>
    {
        public GetAgentByEmailQuery(string email) => Email = email;
        public string Email { get; }
    }
}