using FluentResults;
using Microsoft.EntityFrameworkCore;
using Profile.Domain.Repositories;
using Profile.Persistence.Context;
using Shared.Constants;
using Shared.Models;

namespace Profile.Persistence.Repositories;

public sealed class UserRepository(UserContext context) : IUserRepository
{
    public async Task<Result<User>> GetUserWithProvider(string userEmail)
    {
        try
        {
            User? user = await context.Users
                .Include(u => u.UserOauthProviders)
                .ThenInclude(p => p.Provider)
                .FirstOrDefaultAsync(u => u.UserEmail == userEmail);

            return user == null ? Result.Fail("User does not exist") : Result.Ok(user);
        }
        catch
        {
            return Result.Fail(ErrorMessage.Exception);
        }    
    }

    public async Task<Result<User>> UpdateUser(User user)
    {
        try
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return Result.Ok(user);
        }
        catch
        {
            return Result.Fail(ErrorMessage.Exception);
        }    
    }
}