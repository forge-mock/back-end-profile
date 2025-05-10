using FluentValidation;
using Shared.Validators;

namespace Profile.Application.DTOs.Validators;

internal sealed class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    internal UserUpdateDtoValidator()
    {
        LoginValidator.AddUsernameRules(RuleFor(x => x.Username));
        LoginValidator.AddEmailRules(RuleFor(x => x.UserEmail));
    }
}