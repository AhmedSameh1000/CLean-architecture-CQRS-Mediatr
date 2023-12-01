using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Authentication.CommonValidator.UserWithFullProperty
{
    public class UserValidator : AbstractValidator<IUserProperty>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        #region Constractor

        public UserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }

        #endregion Constractor

        #region ActionsValidation

        public void ApplyValidationRules()
        {
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty]);

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Address] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Address] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty]);

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Email] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Email] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty]);

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Password] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
              .Equal(x => x.ConfirmPassword).WithMessage(_stringLocalizer[SharedSesourcesKeys.NotMatch]);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.ConfirmPassword]);
        }

        #endregion ActionsValidation
    }
}