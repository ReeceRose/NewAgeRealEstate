using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.User.Model;
using NARE.Application.User.Query.GetAllUsers;
using NARE.Application.User.Query.GetPaginatedResults;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.SearchUsersByEmail
{
    public class SearchUsersByEmailQueryHandler : IRequestHandler<SearchUsersByEmailQuery, PaginatedUsersDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SearchUsersByEmailQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<PaginatedUsersDto> Handle(SearchUsersByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            var users = _mapper.Map<List<ApplicationUserDto>>(result.Where(u => u.Email.Contains(request.Email)));
            var model = new PaginationModel {Count = users.Count};
            return await _mediator.Send(new GetPaginatedResultsQuery(users, model), cancellationToken);
        }
    }
}
