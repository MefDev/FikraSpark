using FikraSparkCore.Domain.Entities;

namespace FikraSparkCore.Domain.Events;

public class IdeaCreatedEvent: BaseEvent
{
    public Idea Idea { get; }
    
    public IdeaCreatedEvent(Idea idea)
    {
        Idea = idea;
    }
}
