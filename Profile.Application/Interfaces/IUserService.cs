using FluentResults;
using Profile.Application.DTOs;

namespace Profile.Application.Interfaces;

public interface IUserService
{
    public Task<Result<List<string>>> GetUserProviders(string userEmail);

    public Task<Result<bool>> UpdateUserInformation(UserUpdateDto userUpdate);

    public Task<Result<bool>> UpdateUserPassword(PasswordUpdateDto passwordUpdate);
}