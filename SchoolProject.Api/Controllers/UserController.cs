using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Feature.User.Commands.Models;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class UserController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(RouterLinks.UserRouting.Register)]
        public async Task<IActionResult> Register(AddUserDto addUserDto)
        {
            var Response = await _mediator.Send(new AddUserCommand { AddUserDto = addUserDto });
            return NewResult(Response);
        }
    }
}