using FikraSparkCore.Application.Common.DTOs;

namespace FikraSparkCore.Application.Ideas.Queries.GetIdea;

public record GetIdeaByIdQuery(int id) : IRequest<IdeaDto?>
{
    
}
