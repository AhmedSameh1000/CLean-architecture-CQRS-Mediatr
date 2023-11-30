using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Emails.DTOs;

namespace SchoolProject.Core.Feature.Emails.Command.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public SendEmailDTO SendEmailDTO { get; set; }
    }
}