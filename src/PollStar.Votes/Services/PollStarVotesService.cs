using PollStar.Votes.Abstractions.DataTransferObjects;
using PollStar.Votes.Abstractions.Services;

namespace PollStar.Votes.Services;

public class PollStarVotesService: IPollStarVotesService
{
    public Task<VotesDto> GetVotesAsync(Guid pollId)
    {
        throw new NotImplementedException();
    }

    public Task<VotesDto> CastVoteAsync(CastVoteDto dto)
    {
        throw new NotImplementedException();
    }
}