using FikraSparkCore.Application.Common.DTOs;
using FikraSparkCore.Application.Common.Models;
using FluentValidation;
namespace FikraSparkCore.Application.Ideas.Queries.GetIdeas;

public record GetIdeasQuery(int PageNumber=1, int PageSize=10, string Sort="latest") : IRequest<PaginatedList<IdeaDto>>
{
    
}

