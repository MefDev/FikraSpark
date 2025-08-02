using FluentValidation;
namespace FikraSparkCore.Application.Ideas.Commands.CreateIdea;

public class CreateIdeaCommandValidator: AbstractValidator<CreateIdeaCommand>
{
    public CreateIdeaCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(2000);
    }
}
