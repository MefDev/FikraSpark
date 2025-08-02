namespace FikraSparkCore.Domain.Entities;

public class Idea: BaseAuditableEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Votes { get; set; } = 0;

    public Idea(string title, string description, int votes)
    {
        Title = title;
        Description = description;
        Votes = votes;
    }
    
    public void Update(string title, string description, int votes)
    {
        Title = title;
        Description = description;
        Votes = votes;
    }
}
