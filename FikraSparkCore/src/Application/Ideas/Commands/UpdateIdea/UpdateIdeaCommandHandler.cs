using FikraSparkCore.Application.Common.Interfaces;

namespace FikraSparkCore.Application.Ideas.Commands.UpdateIdea;

public class UpdateIdeaCommandHandler : IRequestHandler<UpdateIdeaCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateIdeaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateIdeaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Ideas.FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException($"Idea {request.Id} not found");

        entity.Update(request.Title, request.Description);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
