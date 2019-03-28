using FluentValidation;

namespace NARE.Application.Listing.Query.GetAgentListings
{
    public class GetAgentListingsQueryValidator : AbstractValidator<GetAgentListingsQuery>
    {
        public GetAgentListingsQueryValidator()
        {
            RuleFor(c => c.AgentId)
                .NotEmpty().WithMessage("Agent ID required")
                .NotNull().WithMessage("Agent ID required");

            RuleFor(l => l.PaginationModel)
                .NotNull().WithMessage("Pagination model required");
        }
    }
}
