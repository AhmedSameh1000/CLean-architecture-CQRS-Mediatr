using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Feature.User.Commands.Models;
using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Core.Feature.User.Queries.Models;
using SchoolProject.Core.Wrappers;
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

        [HttpGet(RouterLinks.UserRouting.GetAll)]
        public async Task<IActionResult> GetUsers([FromQuery] RequestParams requestParams)
        {
            var Response = await _mediator.Send(new GetusersListQuery { RequestParams = requestParams });
            return NewResult(Response);
        }

        [HttpGet(RouterLinks.UserRouting.GetById)]
        public async Task<IActionResult> GetUser([FromRoute] int Id)
        {
            var Response = await _mediator.Send(new GetuserByIdQuery(Id));
            return NewResult(Response);
        }

        [HttpPut(RouterLinks.UserRouting.Updateuser)]
        public async Task<IActionResult> GetUsers(UpdateUserDto updateUserDto)
        {
            var Response = await _mediator.Send(new UpdateUserCommand { UpdateUserDto = updateUserDto });
            return NewResult(Response);
        }

        [HttpDelete(RouterLinks.UserRouting.DeleteUser)]
        public async Task<IActionResult> Deleteuser([FromRoute] int Id)
        {
            var Response = await _mediator.Send(new DeleteuserCommand(Id));
            return NewResult(Response);
        }
    }
}