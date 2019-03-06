﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NARE.Application.Agent.Model;
using NARE.Application.Agent.Query.GenerateLoginToken;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Application.Agent.Query.GetAgentClaim;
using NARE.Domain.Exceptions;

namespace NARE.Application.Agent.Query.LoginAgent
{
    public class LoginAgentQueryHandler : IRequestHandler<LoginAgentQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<Domain.Entities.Agent> _signInManager;
        private readonly UserManager<Domain.Entities.Agent> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginAgentQueryHandler> _logger;


        public LoginAgentQueryHandler(IMediator mediator, SignInManager<Domain.Entities.Agent> signInManager, UserManager<Domain.Entities.Agent> userManager, IMapper mapper, ILogger<LoginAgentQueryHandler> logger)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<string> Handle(LoginAgentQuery request, CancellationToken cancellationToken)
        {
            var agent = _mediator.Send(new GetAgentByEmailQuery(request.Email), cancellationToken).Result;
            
            if (agent == null)
            {
                _logger.LogInformation($"LoginAgent: {request.Email}: Failed login: Agent does not exist");
                throw new InvalidCredentialException();
            }

            var mappedAgent = _mapper.Map<AgentDto>(agent);
            
            var result = await _signInManager.CheckPasswordSignInAsync(agent, request.Password, true);

            if (result.IsLockedOut || !agent.AccountEnabled)
            {
                _logger.LogInformation($"LoginAgent: {request.Email}: Failed login: Account is locked out");
                throw new AccountLockedException();
            }

            if (!(result.Succeeded))
            {
                _logger.LogInformation($"LoginAgent: {request.Email}: Failed login: Invalid credentials");
                throw new InvalidCredentialException();
            }

            var claims = _mediator.Send(new GetAgentClaimQuery(mappedAgent), cancellationToken).Result;

            _logger.LogInformation($"LoginAgent: {agent.Email}: Successful login");

            return await _mediator.Send(new GenerateLoginTokenQuery(claims), cancellationToken);
        }
    }
}