using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Authentication.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Authentication.Commands.Validator
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordlDTO>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(c => c.Email).
                NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
        }
    }
}