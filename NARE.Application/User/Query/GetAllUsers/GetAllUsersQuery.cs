using System.Collections.Generic;
using MediatR;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<ApplicationUser>>
    {

    }
}
