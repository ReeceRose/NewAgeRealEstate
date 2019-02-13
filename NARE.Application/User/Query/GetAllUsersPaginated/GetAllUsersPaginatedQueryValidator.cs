using FluentValidation;

namespace NARE.Application.User.Query.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedQueryValidator : AbstractValidator<GetAllUsersPaginatedQuery>
    {
        public GetAllUsersPaginatedQueryValidator()
        {
            RuleFor(p => p.PaginationModel)
                .NotNull().WithMessage("Pagination model required");
        }
    }
}
