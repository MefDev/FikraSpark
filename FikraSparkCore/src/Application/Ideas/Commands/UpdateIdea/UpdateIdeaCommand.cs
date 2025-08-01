namespace FikraSparkCore.Application.Ideas.Commands;

public record UpdateIdeaCommand(int Id, string Title, string Description) : IRequest;
