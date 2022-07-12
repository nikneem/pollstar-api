using PollStar.Users.Abstractions.DataTransferObjects;
using PollStar.Users.Abstractions.Repositories;
using PollStar.Users.Abstractions.Services;

namespace PollStar.Users.Services;

public class PollStarUsersService : IPollStarUsersService
{
    private readonly IPollStarUsersRepository _repository;

    public async Task<UserDto> Create()
    {
        var userId = Guid.NewGuid();
        if (await _repository.CreateAsync(userId))
        {
            return new UserDto
            {
                UserId = userId
            };
        }

        throw new Exception("Failed to register new user");
    }

    public  Task<UserDto> Restore(Guid userId)
    {
        return _repository.GetAsync(userId);
    }


    public PollStarUsersService(IPollStarUsersRepository repository)
    {
        _repository = repository;
    }

}