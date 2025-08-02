namespace FikraSparkCore.Application.Ideas.Commands.VoteIdea;

public record VoteIdeaCommand(int Id, int Delta) : IRequest;
