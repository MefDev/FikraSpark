namespace FikraSparkCore.Application.Ideas.Commands.UpdateIdea;

public record UpdateIdeaCommand(int Id, string Title, string Description) : IRequest;
