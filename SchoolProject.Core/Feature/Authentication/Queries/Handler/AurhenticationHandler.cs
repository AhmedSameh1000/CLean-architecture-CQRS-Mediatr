using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.DTOs;
using SchoolProject.Core.Feature.Authentication.Queries.Models;
using SchoolProject.Core.Resources;
using MyUser = SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Feature.Authentication.Queries.Handler
{
    public class AurhenticationHandler : ResponseHandler, IRequestHandler<ConfirmEmailQuery, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IValidator<ConfirmEmailDTO> _confirmEmailvalidator;
        private readonly UserManager<MyUser.User> _userManager;

        public AurhenticationHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IValidator<ConfirmEmailDTO> ConfirmEmailvalidator
            , UserManager<MyUser.User> userManager
            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _confirmEmailvalidator = ConfirmEmailvalidator;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
            var User = await _userManager.FindByIdAsync(request.ConfirmEmailDTO.userId);
            if (User is null)
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
            var ConfimedEmail = await _userManager.ConfirmEmailAsync(User, request.ConfirmEmailDTO.Code);

            if (!ConfimedEmail.Succeeded)
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }

            return Success<string>(_stringLocalizer[SharedSesourcesKeys.Done]);
        }
    }
}