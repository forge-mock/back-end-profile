using FluentValidation;
using Shared.Validators;

namespace Profile.Application.DTOs.Validators;

internal sealed class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    internal UserUpdateDtoValidator()
    {
        LoginValidator.AddEmailRules(RuleFor(x => x.OldUserEmail));
        LoginValidator.AddEmailRules(RuleFor(x => x.NewUserEmail));
        LoginValidator.AddUsernameRules(RuleFor(x => x.Username));
    }
}