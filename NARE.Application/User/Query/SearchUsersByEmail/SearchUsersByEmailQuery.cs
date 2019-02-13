using MediatR;
using NARE.Application.User.Model;

namespace NARE.Application.User.Query.SearchUsersByEmail
{
    public class SearchUsersByEmailQuery : IRequest<PaginatedUsersDto>
    {
        public SearchUsersByEmailQuery(string email) => Email = email;

        public string Email { get; set; }
    }
}
