using Microsoft.Extensions.Logging;
using PollStar.Polls.Abstractions.DataTransferObjects;
using PollStar.Polls.Abstractions.DomainModels;
using PollStar.Polls.Abstractions.Repositories;
using PollStar.Polls.Abstractions.Services;
using PollStar.Polls.DomainModels;
using PollStar.Polls.ErrorCodes;
using PollStar.Polls.Exceptions;

namespace PollStar.Polls.Services;

public class PollStarPollsService : IPollStarPollsService
{
    private readonly ILogger<PollStarPollsService> _logger;
    private readonly IPollStarPollsRepository _repository;

    public async Task<List<PollListItemDto>> GetPollsListAsync(Guid sessionId)
    {
        _logger.LogInformation("Fetching polls in session");
        var pollDomainModels = await _repository.GetListAsync(sessionId);
        _logger.LogInformation("Found {pollsCount} polls in session {sessionId}", pollDomainModels.Count, sessionId);

        var pollsList = new List<PollListItemDto>();
        pollsList.AddRange(pollDomainModels.Select(p => new PollListItemDto(p.Name)
        {
            Id = p.Id,
            Description = p.Description,
            DisplayOrder = p.DisplayOrder
        }));
        return pollsList;
    }

    public async Task<PollDto> GetPollAsync(Guid pollId)
    {
        _logger.LogInformation("Fetching poll details");
        var pollDomainModel = await _repository.GetAsync(pollId);
        return ToPollDto(pollDomainModel);
    }

    public async Task<PollDto> CreatePollAsync(CreatePollDto dto)
    {
        var poll = new Poll(dto.SessionId, dto.Name);
        poll.SetDescription(dto.Description);
        foreach (var option in dto.Options)
        {
            poll.AddOption(new PollOption(option.Name, option.Description, dto.Options.IndexOf(option)));
        }
        if (await _repository.CreateAsync(poll))
        {
            return ToPollDto(poll);
        }

        throw new PollStarPollException(PollStarPollErrorCode.PollPersistenceFailed, 
            "Failed to create poll in data persistence store");
    }
    //public async Task<ISessionDto> UpdateSessionAsync(ISessionDto dto)
    //{
    //    _logger.LogInformation("Creating new poll star session domain model");
    //    var session = new Session(dto.Name);
    //    session.SetDescription(dto.Description);
    //    _logger.LogInformation("Domain model created, now persisting in persistence");
    //    if (await _repository.UpdateAsync(session))
    //    {
    //        return ToSessionDto(session);
    //    }
    //    throw new PollStarSessionException(PollStarSessionErrorCode.SessionPersistenceFailed,
    //        "Failed to update session in data persistence store");
    //}
    //public Task<bool> DeleteSessionAsync(Guid id)
    //{
    //    return _repository.DeleteAsync(id);
    //}

    private static PollDto ToPollDto(IPoll poll)
    {
        var options = new List<PollOptionDto>();
        options.AddRange(poll.Options.OrderBy(x=> x.DisplayOrder).Select(po => new PollOptionDto(po.Name)
        {
            Id = po.Id,
            Description = po.Description,
            DisplayOrder = po.DisplayOrder
        }));

        return new PollDto(poll.Name)
        {
            Id = poll.Id,
            Name = poll.Name,
            DisplayOrder = poll.DisplayOrder,
            Description = poll.Description,
            Options = options
        };
    }

    public PollStarPollsService(ILogger<PollStarPollsService> logger, IPollStarPollsRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
}