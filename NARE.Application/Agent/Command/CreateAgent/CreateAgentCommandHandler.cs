using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace NARE.Application.Agent.Command.CreateAgent
{
    public class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, IdentityResult>
    {
        private readonly UserManager<Domain.Entities.Agent> _userManager;
        private readonly IMapper _mapper;

        public CreateAgentCommandHandler(UserManager<Domain.Entities.Agent> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
        {
            return await _userManager.CreateAsync(_mapper.Map<Domain.Entities.Agent>(request.Agent), request.Password);
        }
    }
}
