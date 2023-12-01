using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.DTOs;

namespace SchoolProject.Core.Feature.Authentication.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public ResetPasswordlDTO ResetPasswordlDTO
        { get; set; }
    }
}