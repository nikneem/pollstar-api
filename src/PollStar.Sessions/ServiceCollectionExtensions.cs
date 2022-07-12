using Microsoft.Extensions.DependencyInjection;
using PollStar.Sessions.Abstractions.Repositories;
using PollStar.Sessions.Abstractions.Services;
using PollStar.Sessions.Repositories;
using PollStar.Sessions.Services;

namespace PollStar.Sessions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPollStarSessions(this IServiceCollection services)
    {
        services.AddTransient<IPollStarSessionsService, PollStarSessionsService>();
        services.AddTransient<IPollStarSessionsRepository, PollStarSessionsRepository>();
        return services;
    }
}