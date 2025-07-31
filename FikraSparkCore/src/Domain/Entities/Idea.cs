namespace FikraSparkCore.Domain.Entities;

public class Idea: BaseAuditableEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }

    public Idea(string title, string description)
    {
        Title = title;
        Description = description;
    }
    
    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
