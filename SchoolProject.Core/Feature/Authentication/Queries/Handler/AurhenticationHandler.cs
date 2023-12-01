using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
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
    public class AurhenticationHandler : ResponseHandler,
        IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<ResetPasswordCodeQuey, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IValidator<ConfirmEmailDTO> _confirmEmailvalidator;
        private readonly UserManager<MyUser.User> _userManager;
        private readonly IValidator<ResetPasswordCodeDTO> _resetPasswordCodeValidator;
        private readonly IEncryptionProvider _encryptionProvider;

        public AurhenticationHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IValidator<ConfirmEmailDTO> ConfirmEmailvalidator
            , UserManager<MyUser.User> userManager,
            IValidator<ResetPasswordCodeDTO> resetPasswordCodeValidator

            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _confirmEmailvalidator = ConfirmEmailvalidator;
            _userManager = userManager;
            _resetPasswordCodeValidator = resetPasswordCodeValidator;
            _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f"); ;
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var Result = await _confirmEmailvalidator.ValidateAsync(request.ConfirmEmailDTO);
            if (!Result.IsValid)
            {
                return BadRequest<string>(string.Join(",", Result.Errors.Select(c => c.ErrorMessage).ToList()));
            }

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

        public async Task<Response<string>> Handle(ResetPasswordCodeQuey request, CancellationToken cancellationToken)
        {
            var Result = await _resetPasswordCodeValidator.ValidateAsync(request.ResetCode);
            if (!Result.IsValid)
            {
                return BadRequest<string>(string.Join(",", Result.Errors.Select(c => c.ErrorMessage).ToList()));
            }
            var User = await _userManager.FindByEmailAsync(request.ResetCode.Email);
            if (User is null)
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }

            if (request.ResetCode.Code == User.Code)
            {
                return Success<string>(_stringLocalizer[SharedSesourcesKeys.Done]);
            }
            else
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.TryAgainLater]);
            }
        }
    }
}