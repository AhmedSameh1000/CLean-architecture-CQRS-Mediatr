using SchoolProject.Core.Feature.Departments.DTOs;

namespace SchoolProject.Core.Feature.Departments.Queries.Results
{
    public class GetDepartmentByIdResponse
    {
        //public GetDepartmentByIdResponse()
        //{
        //    Students = new List<StudentResponse>();
        //    Subjects = new List<SubjectResponse>();
        //    Instructors = new List<InstructorResponse>();
        //}
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public string MangerName { get; set; }

        public List<StudentResponse> Students { get; set; }
        public List<SubjectResponse> Subjects { get; set; }

        public List<InstructorResponse> Instructors { get; set; }
    }
}