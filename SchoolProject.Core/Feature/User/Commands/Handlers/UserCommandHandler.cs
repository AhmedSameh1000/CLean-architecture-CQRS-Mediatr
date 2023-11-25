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
using UserNameSpace = SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Feature.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<UpdateUserCommand, Response<string>>,
        IRequestHandler<DeleteuserCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<UserNameSpace.User> _userManager;
        private readonly IValidator<AddUserDto> _userValidator;
        private readonly IValidator<UpdateUserDto> _updateuserValidator;

        public UserCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<UserNameSpace.User> userManager,
            IValidator<AddUserDto> UserValidator, IValidator<UpdateUserDto> UpdateuserValidator) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _userValidator = UserValidator;
            _updateuserValidator = UpdateuserValidator;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _userValidator.ValidateAsync(request.AddUserDto);

            if (!ValidationResult.IsValid)
            {
                return BadRequest<string>(string.Join(",", ValidationResult.Errors.Select(c => c.ErrorMessage)));
            }

            var UserExistByEmail = await _userManager.FindByEmailAsync(request.AddUserDto.Email);
            if (UserExistByEmail is not null)
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.EmailIsAlreadyRegisterd]);

            var UserExistByUserName = await _userManager.FindByNameAsync(request.AddUserDto.UserName);
            if (UserExistByUserName is not null)
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.UserNameIsAlreadyRegisterd]);

            var UserToRegister = _mapper.Map<UserNameSpace.User>(request.AddUserDto);

            var Result = await _userManager.CreateAsync(UserToRegister, request.AddUserDto.Password);

            if (!Result.Succeeded)
            {
                return BadRequest<string>(string.Join(',', Result.Errors.Select(c => c.Description)));
            }

            return Created<string>(_stringLocalizer[SharedSesourcesKeys.LogInSuccess]);
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
    }
}