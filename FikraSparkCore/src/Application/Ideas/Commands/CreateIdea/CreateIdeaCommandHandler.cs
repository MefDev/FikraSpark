using FikraSparkCore.Application.Common.Interfaces;
using FikraSparkCore.Domain.Entities;
using FikraSparkCore.Domain.Events;

namespace FikraSparkCore.Application.Ideas.Commands.CreateIdea;

public class CreateIdeaCommandHandler : IRequestHandler<CreateIdeaCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateIdeaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateIdeaCommand request, CancellationToken cancellationToken)
    {
        var entity = new Idea(request.Title, request.Description, 0);
        
        entity.AddDomainEvent(new IdeaCreatedEvent(entity));

        _context.Ideas.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

