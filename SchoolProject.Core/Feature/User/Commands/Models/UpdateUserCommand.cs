using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.User.DTOs;

namespace SchoolProject.Core.Feature.User.Commands.Models
{
    public class UpdateUserCommand : IRequest<Response<string>>
    {
        public UpdateUserDto UpdateUserDto { get; set; }
    }
}