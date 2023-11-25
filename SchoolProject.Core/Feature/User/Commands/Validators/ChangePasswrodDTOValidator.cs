using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.User.Commands.Validators
{
    public class ChangePasswrodDTOValidator : AbstractValidator<ChangePasswordDTO>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ChangePasswrodDTOValidator(
            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
            RuleFor(c => c.CurrentPassword).NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);

            RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Password] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
               .Equal(x => x.ConfirmPassword).WithMessage(_stringLocalizer[SharedSesourcesKeys.NotMatch]);
        }
    }
}