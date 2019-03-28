using System.Collections.Generic;
using NARE.Domain.Entities;

namespace NARE.Application.Agent.Model
{
    public class PaginatedAgentsDto
    {
        public List<AgentDto> Agents { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
