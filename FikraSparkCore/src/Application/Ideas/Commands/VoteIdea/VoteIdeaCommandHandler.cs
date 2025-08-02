using FikraSparkCore.Application.Common.Interfaces;
using FikraSparkCore.Domain.Entities;
using FikraSparkCore.Domain.Events;

namespace FikraSparkCore.Application.Ideas.Commands.VoteIdea;

public class VoteIdeaCommandHandler : IRequestHandler<VoteIdeaCommand>
{
    private readonly IApplicationDbContext _context;

    public VoteIdeaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(VoteIdeaCommand request, CancellationToken cancellationToken)
    {
        var idea = await _context.Ideas
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (idea == null)
        {
            throw new KeyNotFoundException($"Idea with id {request.Id} not found.");
        }
        
        if (request.Delta != 1 && request.Delta != -1)
        {
            throw new InvalidOperationException("Vote value must be +1 or -1.");
        }
        idea.Votes += request.Delta;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}

