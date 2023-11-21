using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapper
{
    public partial class StudentProfile
    {
        public void GetStudentCommandMapping()
        {
            CreateMap<Student, CreateStudentDTO>()
             .ForMember(dest => dest.DepartmentId, Options => Options.MapFrom(src => src.DID)).ReverseMap();

            CreateMap<Student, UpdateStudentDTO>()
             .ForMember(dest => dest.DepartmentId, Options => Options.MapFrom(src => src.DID)).ReverseMap()
             .ForMember(dest => dest.StudentId, options => options.MapFrom(src => src.Id)).ReverseMap();
        }
    }
}