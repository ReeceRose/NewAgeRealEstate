﻿using MediatR;

namespace NARE.Application.User.Command.ForceEmailConfirmation
{
    public class ForceEmailConfirmationCommand : IRequest<bool>
    {
        public ForceEmailConfirmationCommand(string userId) => UserId = userId;

        public string UserId { get; }
    }
}
