﻿using System.Collections.Generic;
using System.Security.Claims;
using MediatR;

namespace NARE.Application.Agent.Query.GenerateLoginToken
{
    public class GenerateLoginTokenQuery : IRequest<string>
    {
        public IList<Claim> Claims { get; }

        public GenerateLoginTokenQuery(IList<Claim> claims) => Claims = claims;
    }
}
