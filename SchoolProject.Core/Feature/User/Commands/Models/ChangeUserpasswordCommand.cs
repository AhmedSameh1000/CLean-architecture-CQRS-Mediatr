using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.User.DTOs;

namespace SchoolProject.Core.Feature.User.Commands.Models
{
    public class ChangeUserpasswordCommand : IRequest<Response<string>>
    {
        public ChangePasswordDTO ChangePasswordDTO { get; set; }
    }
}