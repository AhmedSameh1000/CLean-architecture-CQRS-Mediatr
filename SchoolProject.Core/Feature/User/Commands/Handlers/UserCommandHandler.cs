using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.User.Commands.Models;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Core.Resources;
using UserNameSpace = SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Feature.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<UserNameSpace.User> _userManager;
        private readonly IValidator<AddUserDto> _userValidator;

        public UserCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<UserNameSpace.User> userManager,
            IValidator<AddUserDto> UserValidator) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _userValidator = UserValidator;
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
    }
}