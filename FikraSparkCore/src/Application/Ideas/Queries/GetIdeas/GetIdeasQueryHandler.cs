using FikraSparkCore.Application.Common.DTOs;
using FikraSparkCore.Application.Common.Interfaces;
using FikraSparkCore.Application.Common.Mappings;
using FikraSparkCore.Application.Common.Models;

namespace FikraSparkCore.Application.Ideas.Queries.GetIdeas;

public class GetIdeasQueryHandler : IRequestHandler<GetIdeasQuery, PaginatedList<IdeaDto>>
{
    private readonly IApplicationDbContext _context;

    public GetIdeasQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<IdeaDto>> Handle(GetIdeasQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Ideas.AsQueryable();
        
        query = request.Sort.ToLower() switch
        {
            "top" => query.OrderByDescending(i => i.Votes),
            _ => query.OrderByDescending(i => i.Created)
        };
        return await query
            .Select(i => new IdeaDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Votes = i.Votes
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}

