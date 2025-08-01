using FikraSparkCore.Application.Common.DTOs;
using FikraSparkCore.Application.Common.Models;
using FikraSparkCore.Application.Ideas.Commands.CreateIdea;
using FikraSparkCore.Application.Ideas.Commands.DeleteIdea;
using FikraSparkCore.Application.Ideas.Commands.UpdateIdea;
using FikraSparkCore.Application.Ideas.Queries.GetIdea;
using FikraSparkCore.Application.Ideas.Queries.GetIdeas;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FikraSparkCore.Web.Endpoints;

public class Ideas: EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetIdeas).RequireAuthorization();
        groupBuilder.MapGet(GetIdeaById, "{id:int}").RequireAuthorization();
        groupBuilder.MapPost(CreateIdea).RequireAuthorization();
        groupBuilder.MapPut(UpdateIdea, "{id:int}").RequireAuthorization();
        groupBuilder.MapDelete(DeleteIdea, "{id:int}").RequireAuthorization();
    }
    
    public async Task<Ok<PaginatedList<IdeaDto>>> GetIdeas(ISender sender, [AsParameters] GetIdeasQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Results<Ok<IdeaDto>, NotFound>> GetIdeaById(ISender sender, int id)
    {
        var result = await sender.Send(new GetIdeaByIdQuery(id));
        return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }
   

    public async Task<Created<int>> CreateIdea(ISender sender, CreateIdeaCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/ideas/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateIdea(ISender sender, int id, UpdateIdeaCommand command)
    {
        if (id != command.Id)
            return TypedResults.BadRequest();

        await sender.Send(command);
        return TypedResults.NoContent();
    }
    
    public async Task<NoContent> DeleteIdea(ISender sender, int id)
    {
        await sender.Send(new DeleteIdeaCommand(id));
        return TypedResults.NoContent();
    }
}
