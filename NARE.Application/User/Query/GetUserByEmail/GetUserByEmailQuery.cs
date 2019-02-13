using MediatR;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<ApplicationUser>
    {
        public GetUserByEmailQuery(string email) => Email = email;
        public string Email { get; }
    }
}