using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Core.Feature.Students.Queries.Results;

namespace SchoolProject.Core.Feature.Students.Commands.Models
{
    public class UpdateStudentCommand : IRequest<Response<StudentToReturn>>
    {
        public UpdateStudentDTO UpdateStudentDTO { get; set; }
    }
}