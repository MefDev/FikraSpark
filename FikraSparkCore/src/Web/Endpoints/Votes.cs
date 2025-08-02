using FikraSparkCore.Application.Ideas.Commands.VoteIdea;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FikraSparkCore.Web.Endpoints;

public class Votes: EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("vote", VoteIdea).RequireAuthorization();
    }
    
    public async Task<NoContent> VoteIdea(ISender sender, VoteIdeaCommand command)
    {
        await sender.Send(command);
        return TypedResults.NoContent();
    }
}
