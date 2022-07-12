using HexMaster.DomainDrivenDesign.ChangeTracking;

namespace PollStar.Polls.Abstractions.DomainModels;

public interface IPoll
{
    Guid SessionId { get; }
    string Name { get; }
    string? Description { get; }
    int DisplayOrder { get; }
    IReadOnlyList<IPollOption> Options { get; }
    Guid Id { get; }
    TrackingState TrackingState { get; }
    void SetName(string value);
    void SetDescription(string? value);
    void AddOption(IPollOption option);
    void RemoveOption(Guid id);
}