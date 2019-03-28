using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NARE.Application.Agent.Command.CreateAgent;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Domain.Entities;
using NARE.Domain.Exceptions.Account;

namespace NARE.Application.Agent.Command.NewAgent
{
    public class NewAgentCommandHandler : IRequestHandler<NewAgentCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<NewAgentCommandHandler> _logger;

        public NewAgentCommandHandler(IMediator mediator, IMapper mapper, ILogger<NewAgentCommandHandler> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(NewAgentCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email;
            var user = await _mediator.Send(new GetAgentByEmailQuery(email), cancellationToken);

            if (user != null)
            {
                _logger.LogInformation($"New Agent: {email}: Registration failed: Agent already exists");
                throw new AccountAlreadyExistsException();
            }

            var mappedUser = _mapper.Map<AgentDto>(request);
            mappedUser.AccountEnabled = true;
            mappedUser.UserName = email;

            var result = await _mediator.Send(new CreateAgentCommand(mappedUser, request.Password), cancellationToken);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"New Agent: {email}: Registration failed: {result.Errors}");
                throw new InvalidRegisterException();
            }

            _logger.LogInformation($"New Agent: {email}: Registration successful");
            return await Task.FromResult(result.Succeeded);
        }
    }
}