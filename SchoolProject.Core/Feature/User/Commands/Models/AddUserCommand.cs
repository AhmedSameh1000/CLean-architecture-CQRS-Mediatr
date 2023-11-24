using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.User.DTOs;

namespace SchoolProject.Core.Feature.User.Commands.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {
        public AddUserDto AddUserDto { get; set; }
    }
}