using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Domain.Exceptions.Agent;
using NARE.Persistence;

namespace NARE.Application.Agent.Command.UpdateAgent
{
    public class UpdateAgentCommandHandler : IRequestHandler<UpdateAgentCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public UpdateAgentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
        {
            if (request.Agent == null) { throw new InvalidAgentException(); }
            _context.Users.Update(request.Agent);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
