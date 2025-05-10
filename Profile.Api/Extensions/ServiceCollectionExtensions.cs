using Profile.Domain.Repositories;
using Profile.Persistence.Repositories;

namespace Profile.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}