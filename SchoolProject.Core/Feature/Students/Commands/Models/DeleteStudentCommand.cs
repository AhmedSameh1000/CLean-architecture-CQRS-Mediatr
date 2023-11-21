using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Students.Commands.Models
{
    public class DeleteStudentCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}