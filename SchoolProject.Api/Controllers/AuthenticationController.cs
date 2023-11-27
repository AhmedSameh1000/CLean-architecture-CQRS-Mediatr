using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Feature.Authentication.Commands.Models;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Data.AppMetaData;
using TestApiJWT.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(RouterLinks.UserRouting.Register)]
        public async Task<IActionResult> Register(AddUserDto addUserDto)
        {
            var Response = await _mediator.Send(new AddUserCommand { RegisterUser = addUserDto });
            return NewResult(Response);
        }

        [HttpPost(RouterLinks.UserRouting.LogIn)]
        public async Task<IActionResult> LogIn([FromBody] LogInModel model)
        {
            var Response = await _mediator.Send(new LogInCommand { model = model });
            return NewResult(Response);
        }
    }
}