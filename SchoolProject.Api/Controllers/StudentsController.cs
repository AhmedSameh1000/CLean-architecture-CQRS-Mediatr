using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Feature.Students.Commands.Models;
using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Core.Feature.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    //[Route("api/[controller]/V1")]
    [ApiController]
    public class StudentsController : AppControllerBase
    {
        public readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(RouterLinks.StudentRouting.Collection)]
        public async Task<IActionResult> GetStudentList()
        {
            var response = await _mediator.Send(new GetStudentListQuery());
            return NewResult(response);
        }

        [HttpGet(RouterLinks.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentList(int id)
        {
            var response = await _mediator.Send(new GetStudentByIdQuery(id));

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost(RouterLinks.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDTO command)
        {
            var Currentresponse = await _mediator.Send(new CreateStudentCommand() { CreateStudentDTO = command });

            return NewResult(Currentresponse);
        }

        [HttpPost(RouterLinks.StudentRouting.Update)]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDTO command)
        {
            var Currentresponse = await _mediator.Send(new UpdateStudentCommand() { UpdateStudentDTO = command });

            return NewResult(Currentresponse);
        }

        [HttpDelete(RouterLinks.StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudent([FromRoute] DeleteStudentCommand command)
        {
            var Currentresponse = await _mediator.Send(command);

            return NewResult(Currentresponse);
        }
    }
}