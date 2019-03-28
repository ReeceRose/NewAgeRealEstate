using FluentValidation;

namespace NARE.Application.Listing.Query.GetAllActiveListingsPaginated
{
    public class GetAllActiveListingsPaginatedQueryValidator : AbstractValidator<GetAllActiveListingsPaginatedQuery>
    {
        public GetAllActiveListingsPaginatedQueryValidator()
        {
            RuleFor(l => l.PaginationModel)
                .NotNull().WithMessage("Pagination model required");
        }
    }
}
