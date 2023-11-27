using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.User.Commands.Models;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using UserNameSpace = SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Feature.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<UpdateUserCommand, Response<string>>,
        IRequestHandler<DeleteuserCommand, Response<string>>,
        IRequestHandler<ChangeUserpasswordCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<UserNameSpace.User> _userManager;
        private readonly IValidator<AddUserDto> _userValidator;
        private readonly IValidator<UpdateUserDto> _updateuserValidator;
        private readonly IValidator<ChangePasswordDTO> _changPasswordValidator;
        private readonly IUserService _userService;

        public UserCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<UserNameSpace.User> userManager,
            IValidator<AddUserDto> UserValidator,
            IValidator<UpdateUserDto> UpdateuserValidator,
            IValidator<ChangePasswordDTO> changPasswordValidator,
            IUserService userService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _userValidator = UserValidator;
            _updateuserValidator = UpdateuserValidator;
            _changPasswordValidator = changPasswordValidator;
            _userService = userService;
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var User = await _userManager.FindByIdAsync(request.UpdateUserDto.Id.ToString());

            if (User is null)
            {
                return NotFound<string>(_stringLocalizer[SharedSesourcesKeys.NotFound]);
            }

            if (await _userManager.Users.AnyAsync(c => c.Email == request.UpdateUserDto.Email && request.UpdateUserDto.Email != User.Email))
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.EmailIsAlreadyRegisterd]);
            }
            if (await _userManager.Users.AnyAsync(c => c.UserName == request.UpdateUserDto.UserName && request.UpdateUserDto.UserName != User.UserName))
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.UserNameIsAlreadyRegisterd]);
            }

            var validationResult = await _updateuserValidator.ValidateAsync(request.UpdateUserDto);

            if (!validationResult.IsValid)
            {
                return BadRequest<string>(string.Join(",", validationResult.Errors.Select(c => c.ErrorMessage)));
            }
            _mapper.Map(request.UpdateUserDto, User);

            var res = await _userManager.UpdateAsync(User);

            if (!res.Succeeded)
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
            return Success<string>(_stringLocalizer[SharedSesourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteuserCommand request, CancellationToken cancellationToken)
        {
            var User = await _userManager.FindByIdAsync(request.Id.ToString());

            if (User is null)
            {
                return NotFound<string>(_stringLocalizer[SharedSesourcesKeys.NotFound]);
            }
            var Result = await _userManager.DeleteAsync(User);

            if (!Result.Succeeded)
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
            return Deleted<string>();
        }

        public async Task<Response<string>> Handle(ChangeUserpasswordCommand request, CancellationToken cancellationToken)
        {
            var ResultValidator = await _changPasswordValidator.ValidateAsync(request.ChangePasswordDTO);

            if (!ResultValidator.IsValid)
            {
                return BadRequest<string>(string.Join(",", ResultValidator.Errors.Select(c => c.ErrorMessage)));
            }

            var user = await _userManager.FindByIdAsync(request.ChangePasswordDTO.Id.ToString());

            if (user is null)
                return NotFound<string>(_stringLocalizer[SharedSesourcesKeys.NotFound]);

            var PasswordIsCorrect = await _userManager.CheckPasswordAsync(user, request.ChangePasswordDTO.CurrentPassword);

            if (!PasswordIsCorrect)
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.PasswordIsUnCorrect]);
            }

            var result = await _userManager.ChangePasswordAsync(user, request.ChangePasswordDTO.CurrentPassword, request.ChangePasswordDTO.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }

            return Success<string>(_stringLocalizer[SharedSesourcesKeys.Done]);
        }
    }
}