using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.DTOs;

namespace SchoolProject.Core.Feature.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public AddRoleDto AddRoleDto { get; set; }
    }
}