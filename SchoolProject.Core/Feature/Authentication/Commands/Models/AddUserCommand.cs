using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.User.DTOs;

namespace SchoolProject.Core.Feature.Authentication.Commands.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {
        public AddUserDto RegisterUser { get; set; }
    }
}