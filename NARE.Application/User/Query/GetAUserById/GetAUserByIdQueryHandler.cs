using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.User.Model;
using NARE.Application.User.Query.GetUserById;

namespace NARE.Application.User.Query.GetAUserById
{
    public class GetAUserByIdQueryHandler : IRequestHandler<GetAUserByIdQuery, ApplicationUserDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAUserByIdQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<ApplicationUserDto> Handle(GetAUserByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ApplicationUserDto>(await _mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken));
        }
    }
}
