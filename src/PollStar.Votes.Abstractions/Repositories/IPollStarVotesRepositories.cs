﻿using PollStar.Votes.Abstractions.DataTransferObjects;

namespace PollStar.Votes.Abstractions.Repositories;

public interface IPollStarVotesRepositories
{
    Task<VotesDto> GetSessionVotesAsync(Guid pollId);
    Task<VotesDto> CastVoteAsync(CastVoteDto dto);
}