namespace FikraSparkCore.Application.Ideas.Queries.GetIdeas;

public class IdeaDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
}
