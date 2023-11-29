using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.Feature.Authorization.DTOs;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthorizationController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(RouterLinks.UserRouting.CreateRole)]
        public async Task<IActionResult> CreateRole([FromBody] AddRoleDto addRoleDto)
        {
            var Response = await _mediator.Send(new AddRoleCommand { AddRoleDto = addRoleDto });
            return NewResult(Response);
        }
    }
}