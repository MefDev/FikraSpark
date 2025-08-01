using FikraSparkCore.Application.Common.DTOs;
using FikraSparkCore.Application.Common.Interfaces;

namespace FikraSparkCore.Application.Ideas.Queries.GetIdea;

public class GetIdeaByIdQueryHandler : IRequestHandler<GetIdeaByIdQuery, IdeaDto?>
{
    private readonly IApplicationDbContext _context;

    public GetIdeaByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IdeaDto?> Handle(GetIdeaByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Ideas
            .Where(i => i.Id == request.id)
            .Select(i => new IdeaDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}

