using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.User.Model;

namespace NARE.Application.User.Query.GetPaginatedResults
{
    public class GetPaginatedResultsQueryHandler : IRequestHandler<GetPaginatedResultsQuery, PaginatedUsersDto>
    {
        public Task<PaginatedUsersDto> Handle(GetPaginatedResultsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new PaginatedUsersDto()
            {
                PaginationModel = request.PaginationModel,
                Users = request.Users
            });
        }
    }
}
