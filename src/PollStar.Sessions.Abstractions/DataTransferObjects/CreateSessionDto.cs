
namespace PollStar.Sessions.Abstractions.DataTransferObjects;

public class CreateSessionDto 
{
    public Guid UserId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
}