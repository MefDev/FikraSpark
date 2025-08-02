namespace FikraSparkCore.Application.Common.DTOs;

public class IdeaDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Votes { get; set; } = 0;   
}
