using FluentResults;
using Shared.Models;

namespace Profile.Domain.Repositories;

public interface IUserRepository
{
    public Task<Result<User?>> GetUserWithProvider(string userEmail);

    public Task<Result<User>> UpdateUserInformation(User user, string username, string userEmail);

    public Task<Result<User>> UpdateUserPassword(User user, string password);
}