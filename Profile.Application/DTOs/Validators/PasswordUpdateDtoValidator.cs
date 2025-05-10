using FluentValidation;
using Shared.Validators;

namespace Profile.Application.DTOs.Validators;

internal sealed class PasswordUpdateDtoValidator : AbstractValidator<PasswordUpdateDto>
{
    internal PasswordUpdateDtoValidator()
    {
        LoginValidator.AddPasswordRules(RuleFor(x => x.OldPassword));
        LoginValidator.AddPasswordRules(RuleFor(x => x.NewPassword));
    }
}