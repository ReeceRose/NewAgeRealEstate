using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NARE.Application.Agent.Query.GenerateLoginToken;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Application.Agent.Query.GetAgentClaim;
using NARE.Domain.Entities;
using NARE.Domain.Exceptions.Account;

namespace NARE.Application.Agent.Query.LoginAgent
{
    public class LoginAgentQueryHandler : IRequestHandler<LoginAgentQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<Domain.Entities.Agent> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginAgentQueryHandler> _logger;


        public LoginAgentQueryHandler(IMediator mediator, SignInManager<Domain.Entities.Agent> signInManager, IMapper mapper, ILogger<LoginAgentQueryHandler> logger)
        {
            _mediator = mediator;
            _signInManager = signInManager;
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

            var claims = await _mediator.Send(new GetAgentClaimQuery(mappedAgent), cancellationToken) ?? new List<Claim>();

            _logger.LogInformation($"LoginAgent: {agent.Email}: Successful login");

            claims.Add(new Claim("Id", agent.Id) );

            return await _mediator.Send(new GenerateLoginTokenQuery(claims), cancellationToken);
        }
    }
}