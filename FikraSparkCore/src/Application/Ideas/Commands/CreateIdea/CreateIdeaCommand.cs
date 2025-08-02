namespace FikraSparkCore.Application.Ideas.Commands.CreateIdea;

public record VoteIdeaCommand(string Title, string Description) : IRequest<int>
{
    
}
