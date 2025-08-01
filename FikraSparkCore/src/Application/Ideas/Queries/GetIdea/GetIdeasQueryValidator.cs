using FikraSparkCore.Application.Ideas.Queries.GetIdeas;
using FluentValidation;
namespace FikraSparkCore.Application.Ideas.Queries.GetIdea;

public class GetIdeasQueryValidator : AbstractValidator<GetIdeasQuery>
{
    public GetIdeasQueryValidator()
    {
        RuleFor(x => x.pageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.pageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100.");
    }

}
