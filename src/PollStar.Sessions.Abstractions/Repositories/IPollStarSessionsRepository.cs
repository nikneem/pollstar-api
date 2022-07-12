using PollStar.Sessions.Abstractions.DomainModels;

namespace PollStar.Sessions.Abstractions.Repositories;

public interface IPollStarSessionsRepository
{
    Task<ISession> GetAsync(Guid sessionId);
    Task<bool> CreateAsync(ISession domainModel);
    Task<bool> UpdateAsync(ISession domainModel);
    Task<bool> DeleteAsync(Guid id);
}