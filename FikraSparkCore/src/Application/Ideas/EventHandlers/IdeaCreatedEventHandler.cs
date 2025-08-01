using FikraSparkCore.Domain.Events;
using Microsoft.Extensions.Logging;

namespace FikraSparkCore.Application.Ideas.EventHandlers;

public class IdeaCreatedEventHandler: INotificationHandler<IdeaCreatedEvent>
{
    private readonly ILogger<IdeaCreatedEventHandler> _logger;

    public IdeaCreatedEventHandler(ILogger<IdeaCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(IdeaCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("FikraSpark Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
