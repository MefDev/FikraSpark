using FikraSparkCore.Application.Common.DTOs;
using FikraSparkCore.Application.Common.Models;
using FluentValidation;
namespace FikraSparkCore.Application.Ideas.Queries.GetIdeas;

public record GetIdeasQuery(int pageNumber=1, int pageSize=10) : IRequest<PaginatedList<IdeaDto>>
{
    
}
