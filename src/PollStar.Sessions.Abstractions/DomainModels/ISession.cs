namespace PollStar.Sessions.Abstractions.DomainModels;

public interface ISession
{
    Guid Id { get; }
    Guid UserId { get; }
    string Name { get; }
    string? Description { get; }
    void SetName(string value);
    void SetDescription(string value);
}