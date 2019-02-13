using System.Collections.Generic;
using NARE.Domain.Entities;

namespace NARE.Application.User.Model
{
    public class PaginatedUsersDto
    {
        public List<ApplicationUserDto> Users { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
