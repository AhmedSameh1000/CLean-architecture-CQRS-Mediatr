using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.Feature.Authorization.DTOs;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IValidator<AddRoleDto> _rolevalidator;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RoleCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer
            , IValidator<AddRoleDto> Rolevalidator,
            RoleManager<IdentityRole<int>> roleManager
            ) : base(stringLocalizer)

        {
            _stringLocalizer = stringLocalizer;
            _rolevalidator = Rolevalidator;
            _roleManager = roleManager;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _rolevalidator.ValidateAsync(request.AddRoleDto);
            if (!ValidationResult.IsValid)
            {
                return BadRequest<string>(string.Join(",", ValidationResult.Errors.Select(c => c.ErrorMessage)));
            }
            var result = await _roleManager.CreateAsync(new IdentityRole<int>() { Name = request.AddRoleDto.Name });

            if (result.Succeeded)
            {
                return Success<string>(_stringLocalizer[SharedSesourcesKeys.Created]);
            }
            else
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
        }
    }
}