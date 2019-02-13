using MediatR;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.GetUserById
{
    public class GetUserByIdQuery : IRequest<ApplicationUser>
    {
        public GetUserByIdQuery(string userId) => UserId = userId;
        public string UserId { get; set; }
    }
}
