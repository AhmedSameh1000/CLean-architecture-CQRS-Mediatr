using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Feature.Departments.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class DepartmentsController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet(RouterLinks.DepartmentRouting.Departments)]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var response = await _mediator.Send(new GetDepartmentByIdQuery(Id));

            return NewResult(response);
        }
    }
}