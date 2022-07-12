using Microsoft.Extensions.Logging;
using PollStar.Sessions.Abstractions.DataTransferObjects;
using PollStar.Sessions.Abstractions.DomainModels;
using PollStar.Sessions.Abstractions.Repositories;
using PollStar.Sessions.Abstractions.Services;
using PollStar.Sessions.DomainModels;
using PollStar.Sessions.ErrorCodes;
using PollStar.Sessions.Exceptions;

namespace PollStar.Sessions.Services
{
    public class PollStarSessionsService : IPollStarSessionsService
    {
        private readonly IPollStarSessionsRepository _repository;
        private readonly ILogger<PollStarSessionsService> _logger;

        public async Task<SessionDto> GetSessionAsync(Guid id, Guid userId)
        {
            _logger.LogInformation("Fetching session from repository");
            var session = await _repository.GetAsync(id);
            _logger.LogInformation("Session {name} ({id}) fetched from repository", session.Name, session.Id);
            return ToSessionDto(session, session.UserId.Equals(userId));
        }
        public async Task<SessionDto> CreateSessionAsync(CreateSessionDto dto)
        {
            _logger.LogInformation("Creating new poll star session domain model");
            var session = new Session(dto.UserId,dto.Name);
            session.SetDescription(dto.Description);
            _logger.LogInformation("Domain model created, now persisting in persistence");
            if (await _repository.CreateAsync(session))
            {
                return ToSessionDto(session, session.UserId.Equals(dto.UserId));
            }

            throw new PollStarSessionException(PollStarSessionErrorCode.SessionPersistenceFailed,
                "Failed to create session in data persistence store");
        }
        public async Task<SessionDto> UpdateSessionAsync(SessionDto dto)
        {
            //_logger.LogInformation("Creating new poll star session domain model");
            //var session = new Session(dto.Name);
            //session.SetDescription(dto.Description);
            //_logger.LogInformation("Domain model created, now persisting in persistence");
            //if (await _repository.UpdateAsync(session))
            //{
            //    return ToSessionDto(session);
            //}
            //throw new PollStarSessionException(PollStarSessionErrorCode.SessionPersistenceFailed,
            //    "Failed to update session in data persistence store");
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSessionAsync(Guid id)
        {
            return _repository.DeleteAsync(id);
        }

        private static SessionDto ToSessionDto(ISession session, bool isOwner)
        {
            return new SessionDto
            {
                Id = session.Id,
                Name = session.Name,
                Description = session.Description,
                IsOwner = isOwner
            };
        }

        public PollStarSessionsService(IPollStarSessionsRepository repository, ILogger<PollStarSessionsService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
