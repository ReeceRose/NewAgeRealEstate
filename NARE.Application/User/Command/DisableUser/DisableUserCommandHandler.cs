﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NARE.Application.User.Command.UpdateUser;
using NARE.Application.User.Query.GetUserById;
using NARE.Domain.Exceptions;

namespace NARE.Application.User.Command.DisableUser
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DisableUserCommandHandler> _logger;

        public DisableUserCommandHandler(IMediator mediator, ILogger<DisableUserCommandHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken);
            // Should not be the case as the ID is not being touched by the user at all
            if (user == null)
            {
                throw new InvalidUserException();
            }

            user.AccountEnabled = false;
            _logger.LogInformation($"Disable User: {request.UserId}: Account disabled");
            await _mediator.Send(new UpdateUserCommand(user), cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
