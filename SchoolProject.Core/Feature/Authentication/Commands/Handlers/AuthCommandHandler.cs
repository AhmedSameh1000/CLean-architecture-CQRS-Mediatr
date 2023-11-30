using AutoMapper;
using FluentValidation;
using JWTApi.Models;
using JWTApi.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.Commands.Models;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;
        private readonly IEmailServices _emailServices;
        private readonly IUrlHelper _urlHelper;

        public AuthCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<data.User> userManager,
            IValidator<AddUserDto> UserValidator,
            IValidator<LogInModel> LogInvalidator,
            IHttpContextAccessor httpContextAccessor,
            IAuthService authService,
            IEmailServices emailServices, IUrlHelper urlHelper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _userValidator = UserValidator;
            _logInvalidator = LogInvalidator;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
            _emailServices = emailServices;
            _urlHelper = urlHelper;
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

            //GenrateCode
            var MyCode = await _userManager.GenerateEmailConfirmationTokenAsync(UserToRegister);
            var Req = _httpContextAccessor.HttpContext.Request;

            var returnUrl = Req.Scheme + "://" + Req.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = UserToRegister.Id, Code = MyCode });
            var message = $"To Confirm Email Click Link: <a href='{returnUrl}'></a>";

            //SendCodeToEmail
            var SendEmailResult = await _emailServices.SendEmail(UserToRegister.Email, returnUrl, "Confirm Your Email");
            if (!SendEmailResult)
            {
                //DeleteThisUser
                await _userManager.DeleteAsync(UserToRegister);
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.TryAgainLater]);
            }

            return Created<string>(_stringLocalizer[SharedSesourcesKeys.Created]);
        }

        public async Task<Response<AuthModel>> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _logInvalidator.ValidateAsync(request.model);

            if (!ValidationResult.IsValid)
            {
                return BadRequest<AuthModel>(string.Join(",", ValidationResult.Errors.Select(c => c.ErrorMessage)));
            }
            var User = await _userManager.FindByEmailAsync(request.model.Email);
            if (User is null)
            {
                return BadRequest<AuthModel>(_stringLocalizer[SharedSesourcesKeys.NotFound]);
            }
            if (!User.EmailConfirmed)
            {
                return BadRequest<AuthModel>(_stringLocalizer[SharedSesourcesKeys.ConfirmEmail]);
            }
            AuthModel result = await _authService.GetToken(request.model);

            if (!result.isAuthenticated)
            {
                return BadRequest<AuthModel>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
            return Success(result);
        }
    }
}/*

CfDJ8Flk1rAVURZJrImC7dZ0bGDgjJJmR0RX
sqyhyZCZhoe2I7pWaqgKmEdJzGiQOV6u6w94c2asQ
cuT3SlD/tLHQ8wh1zgol0aU2f5d6fts7rueTTgZI85
Oc5tYR4f0W3U+8XeUCKrPMYNQo2jZOc8QYCj
6PROBa0hyDatOch1H/20QQWsm9ma29FLESO4
V39xgh+syk45YcenmcTrhHHOlYKIQB43LbI
/bt06vTGsPIrfqKwkCJKOZ9YxYZhTCQ/1i5Q==

  */

/*

CfDJ8Flk1rAVURZJrImC7dZ0bGDgjJJmR0RX
sqyhyZCZhoe2I7pWaqgKmEdJzGiQOV6u6w94c2asQ
cuT3SlD/tLHQ8wh1zgol0aU2f5d6fts7rueTTgZI85
Oc5tYR4f0W3U+8XeUCKrPMYNQo2jZOc8QYCj
6PROBa0hyDatOch1H/20QQWsm9ma29FLESO4
V39xgh+syk45YcenmcTrhHHOlYKIQB43LbI
/bt06vTGsPIrfqKwkCJKOZ9YxYZhTCQ/1i5Q==
 */