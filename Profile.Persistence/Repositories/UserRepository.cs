using FluentResults;
using Microsoft.EntityFrameworkCore;
using Profile.Domain.Repositories;
using Profile.Persistence.Context;
using Shared.Constants;
using Shared.Models;

namespace Profile.Persistence.Repositories;

public sealed class UserRepository(UserContext context) : IUserRepository
{
    public async Task<Result<User?>> GetUserWithProvider(string userEmail)
    {
        try
        {
            User? user = await context.Users
                .AsNoTracking()
                .Include(u => u.UserOauthProviders)
                .ThenInclude(p => p.Provider)
                .FirstOrDefaultAsync(u => u.UserEmail == userEmail);

            return Result.Ok(user);
        }
        catch
        {
            return Result.Fail(ErrorMessage.Exception);
        }    
    }

    public async Task<Result<User>> UpdateUserInformation(User user, string username, string userEmail)
    {
        try
        {
            context.Attach(user);
            user.Username = username;
            user.UserEmail = userEmail;
            await context.SaveChangesAsync();
            context.Entry(user).State = EntityState.Detached;
            return Result.Ok(user);
        }
        catch
        {
            return Result.Fail(ErrorMessage.Exception);
        }    
    }

    public async Task<Result<User>> UpdateUserPassword(User user, string password)
    {
        try
        {
            context.Attach(user);
            user.Password = password;
            await context.SaveChangesAsync();
            context.Entry(user).State = EntityState.Detached;
            return Result.Ok(user);
        }
        catch
        {
            return Result.Fail(ErrorMessage.Exception);
        }    
    }
}