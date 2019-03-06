using MediatR;

namespace NARE.Application.Agent.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenQuery : IRequest<string>
    {
        public GenerateResetPasswordTokenQuery(Domain.Entities.Agent agent) => Agent = agent;

        public Domain.Entities.Agent Agent { get; }
    }
}
