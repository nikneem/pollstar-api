using PollStar.Users.Abstractions.DataTransferObjects;

namespace PollStar.Users.Abstractions.Repositories;

public interface IPollStarUsersRepository
{
    Task<UserDto> GetAsync(Guid userId);
    Task<bool> CreateAsync(Guid userId);
}