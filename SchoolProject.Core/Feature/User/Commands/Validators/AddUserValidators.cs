using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.User.CommonValidator.UserWithFullProperty;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.User.Commands.Validators
{
    public class AddUserValidators : AbstractValidator<AddUserDto>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddUserValidators(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            Include(new UserValidator(_stringLocalizer));
        }
    }
}