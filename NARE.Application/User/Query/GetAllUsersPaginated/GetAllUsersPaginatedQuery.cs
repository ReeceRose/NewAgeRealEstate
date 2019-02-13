using MediatR;
using NARE.Application.User.Model;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedQuery : IRequest<PaginatedUsersDto>
    {
        public GetAllUsersPaginatedQuery(PaginationModel model) => PaginationModel = model;

        public PaginationModel PaginationModel { get; set; }
    }
}
