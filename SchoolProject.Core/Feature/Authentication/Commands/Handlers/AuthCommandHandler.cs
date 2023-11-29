using AutoMapper;
using FluentValidation;
using JWTApi.Models;
using JWTApi.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.Commands.Models;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Core.Resources;
using TestApiJWT.Models;
using data = SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Feature.Authentication.Commands.Handlers
{
    public class AuthCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<LogInCommand, Response<AuthModel>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<data.User> _userManager;
        private readonly IValidator<AddUserDto> _userValidator;
        private readonly IValidator<LogInModel> _logInvalidator;
        private readonly IAuthService _authService;

        public AuthCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<data.User> userManager,
            IValidator<AddUserDto> UserValidator,
            IValidator<LogInModel> LogInvalidator,

            IAuthService authService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _userValidator = UserValidator;
            _logInvalidator = LogInvalidator;
            _authService = authService;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _userValidator.ValidateAsync(request.RegisterUser);

            if (!ValidationResult.IsValid)
            {
                return BadRequest<string>(string.Join(",", ValidationResult.Errors.Select(c => c.ErrorMessage)));
            }

            var UserExistByEmail = await _userManager.FindByEmailAsync(request.RegisterUser.Email);
            if (UserExistByEmail is not null)
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.EmailIsAlreadyRegisterd]);

            var UserExistByUserName = await _userManager.FindByNameAsync(request.RegisterUser.UserName);
            if (UserExistByUserName is not null)
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.UserNameIsAlreadyRegisterd]);

            var UserToRegister = _mapper.Map<data.User>(request.RegisterUser);

            var Result = await _userManager.CreateAsync(UserToRegister, request.RegisterUser.Password);

            if (!Result.Succeeded)
            {
                return BadRequest<string>(string.Join(',', Result.Errors.Select(c => c.Description)));
            }

            await _userManager.AddToRoleAsync(UserToRegister, "User");

            return Created<string>(_stringLocalizer[SharedSesourcesKeys.Done]);
        }

        public async Task<Response<AuthModel>> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _logInvalidator.ValidateAsync(request.model);

            if (!ValidationResult.IsValid)
            {
                return BadRequest<AuthModel>(string.Join(",", ValidationResult.Errors.Select(c => c.ErrorMessage)));
            }

            AuthModel result = await _authService.GetToken(request.model);

            if (!result.isAuthenticated)
            {
                return BadRequest<AuthModel>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
            return Success(result);
        }
    }
}