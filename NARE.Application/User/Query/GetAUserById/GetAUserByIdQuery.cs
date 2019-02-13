using MediatR;
using NARE.Application.User.Model;

namespace NARE.Application.User.Query.GetAUserById
{
    public class GetAUserByIdQuery : IRequest<ApplicationUserDto>
    {
        public GetAUserByIdQuery(string userId) => UserId = userId;
        public string UserId { get; set; }
    }
}
