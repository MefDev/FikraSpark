using FikraSparkCore.Application.Ideas.Commands.CreateIdea;

namespace FikraSparkCore.Web.Endpoints;

public static class IdeasEndpoints
{
    public static IEndpointRouteBuilder MapIdeasEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/ideas", async (CreateIdeaCommand command, ISender sender) =>
        {
            var id = await sender.Send(command);
            return Results.Created($"/ideas/{id}", id);
        });

        return routes;
    }
}
