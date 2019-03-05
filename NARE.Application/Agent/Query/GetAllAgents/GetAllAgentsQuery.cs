using System.Collections.Generic;
using MediatR;

namespace NARE.Application.Agent.Query.GetAllAgents
{
    public class GetAllAgentsQuery : IRequest<List<Domain.Entities.Agent>>
    {

    }
}
