using MediatR;
using SchoolProject.Core.Bases;

using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Feature.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<StudentToReturn>>>
    {
        public RequestParams RequestParams { get; set; }
    }
}