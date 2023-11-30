using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Feature.Emails.Command.Models;
using SchoolProject.Core.Feature.Emails.DTOs;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class EmailsController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public EmailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(RouterLinks.UserRouting.SendEmails)]
        public async Task<IActionResult> Register([FromBody] SendEmailDTO sendEmailDTO)
        {
            var Response = await _mediator.Send(new SendEmailCommand { SendEmailDTO = sendEmailDTO });
            return NewResult(Response);
        }
    }
}