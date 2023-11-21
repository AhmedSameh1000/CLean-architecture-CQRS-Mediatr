using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapper
{
    public partial class StudentProfile
    {
        public void GetStudentQueryMapping()
        {
            CreateMap<Student, StudentToReturn>()
             .ForMember(dest => dest.DepartmentName, Options => Options.MapFrom(src => src.Department.DName)).ReverseMap();
        }
    }
}