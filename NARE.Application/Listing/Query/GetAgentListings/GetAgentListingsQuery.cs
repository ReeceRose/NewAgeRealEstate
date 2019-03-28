using MediatR;
using NARE.Application.Listing.Model;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.GetAgentListings
{
    public class GetAgentListingsQuery : IRequest<PaginatedListingsDto>
    {
        public GetAgentListingsQuery(string agentId, PaginationModel paginationModel)
        {
            AgentId = agentId;
            PaginationModel = paginationModel;
        }

        public string AgentId { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
