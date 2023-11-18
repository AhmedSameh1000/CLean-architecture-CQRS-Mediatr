using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Feature.Students.Queries.Models
{
    public class GetStudentByIdQuery : IRequest<Response<StudentToReturn>>
    {
        public int Id { get; set; }

        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}