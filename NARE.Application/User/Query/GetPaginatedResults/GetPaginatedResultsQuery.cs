using System.Collections.Generic;
using MediatR;
using NARE.Application.User.Model;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.GetPaginatedResults
{
    public class GetPaginatedResultsQuery : IRequest<PaginatedUsersDto>
    {
        public GetPaginatedResultsQuery(List<ApplicationUserDto> users, PaginationModel paginationModel)
        {
            Users = users;
            PaginationModel = paginationModel;
        }

        public List<ApplicationUserDto> Users { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
