using FluentValidation;

namespace FikraSparkCore.Application.Ideas.Commands.VoteIdea;

public class VoteIdeaCommandValidator : AbstractValidator<VoteIdeaCommand>
{
    public VoteIdeaCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0)
            .WithMessage("Id must be a positive number.");

        RuleFor(v => v.Delta)
            .Must(value => value == 1 || value == -1)
            .WithMessage("Vote value must be +1 (upvote) or -1 (downvote).");
    }
}
