using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Authentication.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Authentication.Queries.Validator
{
    public class RsetPasswordCodeValidator : AbstractValidator<ResetPasswordCodeDTO>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public RsetPasswordCodeValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(c => c.Code).
                NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
            RuleFor(c => c.Email).
                NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
        }
    }
}