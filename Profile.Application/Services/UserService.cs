using FluentResults;
using FluentValidation.Results;
using Profile.Application.DTOs;
using Profile.Application.DTOs.Validators;
using Profile.Application.Interfaces;
using Profile.Domain.Repositories;
using Shared.Constants;
using Shared.Models;
using Shared.Services;

namespace Profile.Application.Services;

public sealed class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<Result<List<string>>> GetUserProviders(string userEmail)
    {
        try
        {
            Result<User?> result = await userRepository.GetUserWithProvider(userEmail);

            if (result.Value == null)
            {
                return Result.Fail("User does not exist");
            }

            List<string> providers = result.Value.UserOauthProviders.Select(p => p.Provider.Name).ToList();

            return Result.Ok(providers);
        }
        catch
        {
            return Result.Fail(ErrorMessage.Exception);
        }
    }

    public async Task<Result<bool>> UpdateUserInformation(UserUpdateDto userUpdate)
    {
        try
        {
            UserUpdateDtoValidator validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(userUpdate);

            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Fail(errors);
            }

            Result<User?> result = await userRepository.GetUserWithProvider(userUpdate.OldUserEmail);

            if (result.Value == null)
            {
                return Result.Fail("User does not exist");
            }

            await userRepository.UpdateUserInformation(result.Value, userUpdate.Username, userUpdate.NewUserEmail);

            return Result.Ok();
        }
        catch
        {
            return Result.Fail(ErrorMessage.Exception);
        }
    }

    public async Task<Result<bool>> UpdateUserPassword(PasswordUpdateDto passwordUpdate)
    {
        try
        {
            PasswordUpdateDtoValidator validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(passwordUpdate);

            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Fail(errors);
            }

            Result<User?> result = await userRepository.GetUserWithProvider(passwordUpdate.UserEmail);

            if (result.Value == null)
            {
                return Result.Fail("User does not exist");
            }

            bool isPasswordValid = PasswordHasher.Verify(
                passwordUpdate.OldPassword,
                result.Value.Password ?? string.Empty);

            if (!isPasswordValid)
            {
                Result.Fail("Old password is incorrect");
            }

            await userRepository.UpdateUserPassword(result.Value, passwordUpdate.NewPassword);

            return Result.Ok();
        }
        catch
        {
            return Result.Fail(ErrorMessage.Exception);
        }
    }
}