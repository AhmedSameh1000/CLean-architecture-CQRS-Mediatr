using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.User.Commands.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UpdateUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty]);

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Address] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Address] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty]);

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Email] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Email] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty]);
        }
    }
}