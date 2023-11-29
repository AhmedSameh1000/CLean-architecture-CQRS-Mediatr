using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Authorization.CommonValidator.CommonRoleValidator;
using SchoolProject.Core.Feature.Authorization.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Authorization.Commands.Validator
{
    public class RoleValidators : AbstractValidator<AddRoleDto>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RoleValidators(IStringLocalizer<SharedResources> stringLocalizer, RoleManager<IdentityRole<int>> roleManager)
        {
            _stringLocalizer = stringLocalizer;
            _roleManager = roleManager;
            Include(new RolePropertiesValidation(_stringLocalizer, _roleManager));
        }
    }
}