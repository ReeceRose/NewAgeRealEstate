using FluentValidation;

namespace NARE.Application.Listing.Query.GetAllListingsPaginated
{
    public class GetAllListingsPaginatedQueryValidator : AbstractValidator<GetAllListingsPaginatedQuery>
    {
        public GetAllListingsPaginatedQueryValidator()
        {
            RuleFor(l => l.PaginationModel)
                .NotNull().WithMessage("Pagination model required");
        }
    }
}
