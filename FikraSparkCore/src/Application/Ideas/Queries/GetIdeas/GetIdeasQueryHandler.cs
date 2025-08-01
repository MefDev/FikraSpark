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
        return await _context.Ideas
            .OrderBy(i => i.Id)
            .Select(i => new IdeaDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description
            })
            .PaginatedListAsync(request.pageNumber, request.pageNumber);
    }
}

