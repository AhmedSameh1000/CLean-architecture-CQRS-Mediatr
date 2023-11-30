using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Emails.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Emails.Command.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailDTO>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public SendEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);

            RuleFor(c => c.Message)
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
        }
    }
}