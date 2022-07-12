using PollStar.Users.Abstractions.DataTransferObjects;

namespace PollStar.Users.Abstractions.Services;

public interface IPollStarUsersService
{
    Task<UserDto> Create();
    Task<UserDto> Restore(Guid userId);
}