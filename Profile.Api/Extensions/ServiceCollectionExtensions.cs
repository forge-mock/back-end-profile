using Profile.Application.Interfaces;
using Profile.Application.Services;
using Profile.Domain.Repositories;
using Profile.Persistence.Repositories;

namespace Profile.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }

    internal static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}