using MediatR;
using NARE.Application.Agent.Model;
using NARE.Domain.Entities;

namespace NARE.Application.Agent.Query.GetAllAgentsPaginated
{
    public class GetAllAgentsPaginatedQuery : IRequest<PaginatedAgentsDto>
    {
        public GetAllAgentsPaginatedQuery(PaginationModel model) => PaginationModel = model;

        public PaginationModel PaginationModel { get; set; }
    }
}
