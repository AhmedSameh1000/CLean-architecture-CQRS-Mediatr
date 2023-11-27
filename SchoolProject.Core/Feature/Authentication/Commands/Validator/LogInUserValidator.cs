using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;
using TestApiJWT.Models;

namespace SchoolProject.Core.Feature.Authentication.Commands.Validator
{
    public class LogInUserValidator : AbstractValidator<LogInModel>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public LogInUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(c => c.Email).NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
            RuleFor(c => c.Password).NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
        }
    }
}