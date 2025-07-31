namespace FikraSparkCore.Application.Ideas.Commands.CreateIdea;

public record CreateIdeaCommand(string Title, string Description) : IRequest<int>;
