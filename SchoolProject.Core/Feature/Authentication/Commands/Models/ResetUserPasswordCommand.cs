using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.DTOs;

namespace SchoolProject.Core.Feature.Authentication.Commands.Models
{
    public class ResetUserPasswordCommand : IRequest<Response<string>>
    {
        public ResetPasswordDTO ResetPasswordDTO { get; set; }
    }
}