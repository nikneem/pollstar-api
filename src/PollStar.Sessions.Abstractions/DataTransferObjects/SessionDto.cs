
namespace PollStar.Sessions.Abstractions.DataTransferObjects;

public class SessionDto 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsOwner { get; set; }
}