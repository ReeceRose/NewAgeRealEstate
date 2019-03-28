using FluentValidation;

namespace NARE.Application.Listing.Query.GetListingById
{
    public class GetListingByIdQueryValidator : AbstractValidator<GetListingByIdQuery>
    {
        public GetListingByIdQueryValidator()
        {
            RuleFor(l => l.Id)
                .NotEmpty().WithMessage("Listing ID required")
                .NotNull().WithMessage("Listing ID required");
        }
    }
}
