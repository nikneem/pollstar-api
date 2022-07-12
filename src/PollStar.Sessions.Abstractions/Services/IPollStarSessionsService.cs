using PollStar.Sessions.Abstractions.DataTransferObjects;

namespace PollStar.Sessions.Abstractions.Services;

public interface IPollStarSessionsService
{
    Task<SessionDto> GetSessionAsync(Guid id, Guid userId);
    Task<SessionDto> CreateSessionAsync(CreateSessionDto dto);
    Task<SessionDto> UpdateSessionAsync(SessionDto dto);
    Task<bool> DeleteSessionAsync(Guid id);
}