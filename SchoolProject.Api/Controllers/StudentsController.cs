using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Feature.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    //[Route("api/[controller]/V1")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(RouterLinks.StudentRouting.Collection)]
        public async Task<IActionResult> GetStudentList()
        {
            var response = await _mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }

        [HttpGet(RouterLinks.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentList(int id)
        {
            var response = await _mediator.Send(new GetStudentByIdQuery(id));
            return Ok(response);
        }
    }
}