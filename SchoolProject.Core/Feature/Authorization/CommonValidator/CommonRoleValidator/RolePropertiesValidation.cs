using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Authorization.CommonValidator.CommonRoleValidator
{
    public class RolePropertiesValidation : AbstractValidator<IRolePropertiesValidation>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolePropertiesValidation(

            IStringLocalizer<SharedResources> stringLocalizer, RoleManager<IdentityRole> roleManager)
        {
            _stringLocalizer = stringLocalizer;
            _roleManager = roleManager;
            ValidateProperties();
        }

        public void ValidateProperties()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage(_stringLocalizer[SharedSesourcesKeys.Required]);
            RuleFor(c => c.Name)
                .NotNull()
                .WithMessage(_stringLocalizer[SharedSesourcesKeys.Required]);

            RuleFor(c => c.Name)
               .MustAsync(async (Name, CancellationToken) =>
                  !await isExist(Name)
               ).WithMessage(_stringLocalizer[SharedSesourcesKeys.DuplicateRoleName]);
        }

        public async Task<bool> isExist(string roleName)
        {
            if (await _roleManager.Roles.AnyAsync(c => c.Name.ToLower() == roleName.ToLower()))
                return true;
            else
                return false;
        }
    }
}