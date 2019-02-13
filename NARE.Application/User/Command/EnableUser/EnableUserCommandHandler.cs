using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NARE.Application.User.Command.UpdateUser;
using NARE.Application.User.Query.GetUserById;
using NARE.Domain.Exceptions;

namespace NARE.Application.User.Command.EnableUser
{
    public class EnableUserCommandHandler: IRequestHandler<EnableUserCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EnableUserCommandHandler> _logger;

        public EnableUserCommandHandler(IMediator mediator, ILogger<EnableUserCommandHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> Handle(EnableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken);
            // Should not be the case as the ID is not being touched by the user at all
            if (user == null)
            {
                throw new InvalidUserException();
            }

            user.AccountEnabled = true;
            _logger.LogInformation($"Enable User: {request.UserId}: Account enabled");
            await _mediator.Send(new UpdateUserCommand(user), cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
