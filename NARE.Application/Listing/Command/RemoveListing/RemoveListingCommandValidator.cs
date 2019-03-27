using FluentValidation;

namespace NARE.Application.Listing.Command.RemoveListing
{
    public class RemoveListingCommandValidator : AbstractValidator<RemoveListingCommand>
    {
        public RemoveListingCommandValidator()
        {
            RuleFor(l => l.ListingId)
                .NotEmpty().WithMessage("Listing ID required")
                .NotNull().WithMessage("Listing ID required");
        }
    }
}
