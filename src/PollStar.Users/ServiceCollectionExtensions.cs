using Microsoft.Extensions.DependencyInjection;
using PollStar.Users.Abstractions.Repositories;
using PollStar.Users.Abstractions.Services;
using PollStar.Users.Repositories;
using PollStar.Users.Services;

namespace PollStar.Users;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPollStarUsers(this IServiceCollection services)
    {
        services.AddTransient<IPollStarUsersService, PollStarUsersService>();
        services.AddTransient<IPollStarUsersRepository, PollStarUsersRepository>();
        return services;
    }
}