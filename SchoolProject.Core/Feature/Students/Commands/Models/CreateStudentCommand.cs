using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Core.Feature.Students.Queries.Results;

namespace SchoolProject.Core.Feature.Students.Commands.Models
{
    public class CreateStudentCommand : IRequest<Response<StudentToReturn>>
    {
        public CreateStudentDTO CreateStudentDTO { get; set; }
    }
}