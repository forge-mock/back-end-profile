using Profile.Application.Interfaces;
using Profile.Application.Services;
using Profile.Domain.Repositories;
using Profile.Persistence.Repositories;
using Shared.Interfaces;
using Shared.Services;

namespace Profile.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenParser, TokenParser>();
    }

    internal static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}