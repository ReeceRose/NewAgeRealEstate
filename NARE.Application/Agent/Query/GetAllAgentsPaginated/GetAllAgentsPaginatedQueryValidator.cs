using FluentValidation;

namespace NARE.Application.Agent.Query.GetAllAgentsPaginated
{
    public class GetAllAgentsPaginatedQueryValidator : AbstractValidator<GetAllAgentsPaginatedQuery>
    {
        public GetAllAgentsPaginatedQueryValidator()
        {
            RuleFor(p => p.PaginationModel)
                .NotNull().WithMessage("Pagination model required");
        }
    }
}
