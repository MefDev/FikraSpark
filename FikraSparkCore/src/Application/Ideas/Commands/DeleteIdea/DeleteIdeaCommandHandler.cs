using FikraSparkCore.Application.Common.Interfaces;

namespace FikraSparkCore.Application.Ideas.Commands.DeleteIdea;

public class DeleteIdeaCommandHandler : IRequestHandler<DeleteIdeaCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteIdeaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteIdeaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Ideas.FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException($"Idea {request.Id} not found");

        _context.Ideas.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
