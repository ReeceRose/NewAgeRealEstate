﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Domain.Exceptions;

namespace NARE.Application.Agent.Command.RemoveAgent
{
    public class RemoveAgentCommandHandler : IRequestHandler<RemoveAgentCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<Domain.Entities.Agent> _userManager;

        public RemoveAgentCommandHandler(IMediator mediator, UserManager<Domain.Entities.Agent> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<bool> Handle(RemoveAgentCommand request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(new GetAgentByIdQuery(request.AgentId), cancellationToken);
            if (agent == null)
            {
                throw new InvalidAgentException();
            }

            var result = await _userManager.DeleteAsync(agent);

            return await Task.FromResult(result.Succeeded);
        }
    }
}
