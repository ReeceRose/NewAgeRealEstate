using MediatR;

namespace NARE.Application.Listing.Query.GetListingCount
{
    public class GetListingCountQuery : IRequest<int>
    {
        public GetListingCountQuery(string agentId) => AgentId = agentId;
        public string AgentId { get; set; }
    }
}
