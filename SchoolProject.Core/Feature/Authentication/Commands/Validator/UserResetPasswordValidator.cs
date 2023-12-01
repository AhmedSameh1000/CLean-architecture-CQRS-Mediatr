using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Authentication.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Authentication.Commands.Validator
{
    public class UserResetPasswordValidator : AbstractValidator<ResetPasswordDTO>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UserResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(c => c.Email).
             NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
            .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);

            RuleFor(c => c.Password).
             NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
            .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
            .Equal(c => c.ConfirmPassword).WithMessage(_stringLocalizer[SharedSesourcesKeys.NotMatch]);

            RuleFor(c => c.ConfirmPassword).
             NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
            .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
        }
    }
}