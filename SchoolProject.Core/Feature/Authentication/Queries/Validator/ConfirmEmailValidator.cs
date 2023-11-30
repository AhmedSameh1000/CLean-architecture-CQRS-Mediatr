using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Authentication.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Authentication.Queries.Validator
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailDTO>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ConfirmEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            voidApplyValidation();
        }

        public void voidApplyValidation()
        {
            RuleFor(c => c.Code)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);

            RuleFor(c => c.userId)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
        }
    }
}