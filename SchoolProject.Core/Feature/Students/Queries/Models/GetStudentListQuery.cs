using MediatR;
using SchoolProject.Core.Bases;

using SchoolProject.Core.Feature.Students.Queries.Results;

namespace SchoolProject.Core.Feature.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<StudentToReturn>>>
    {
    }
}