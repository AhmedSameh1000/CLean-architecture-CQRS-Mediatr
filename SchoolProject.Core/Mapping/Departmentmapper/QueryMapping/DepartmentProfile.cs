using SchoolProject.Core.Feature.Departments.DTOs;
using SchoolProject.Core.Feature.Departments.Queries.Results;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.CommonLocalizer;

namespace SchoolProject.Core.Mapping.Departmentmapper
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentQueryMapping()
        {
            CreateMap<Student, StudentResponse>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => c.StudentId))
                .ForMember(c => c.Name, opt => opt.MapFrom(c => LocalizeEntities.GetLicalizedName(c.NameAr, c.NameEn)));

            CreateMap<Subject, SubjectResponse>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => c.SubId))
                .ForMember(c => c.Name, opt => opt.MapFrom(c => c.SubName));

            CreateMap<Instructor, InstructorResponse>()
               .ForMember(c => c.Id, opt => opt.MapFrom(c => c.Id))
               .ForMember(c => c.Name, opt => opt.MapFrom(c => LocalizeEntities.GetLicalizedName(c.InstructorNameAr, c.InstructorNameEn)));

            CreateMap<Department, GetDepartmentByIdResponse>()
                    .ForMember(c => c.Id, opt => opt.MapFrom(c => c.DID))
                    .ForMember(c => c.MangerName, opt => opt.MapFrom(c => LocalizeEntities.GetLicalizedName(c.Instructor.InstructorNameAr, c.Instructor.InstructorNameEn)))
                    .ForMember(c => c.DepartmentName, opt => opt.MapFrom(c => LocalizeEntities.GetLicalizedName(c.DNameAr, c.DNameEn)))
                    .ForMember(c => c.Subjects, opt => opt.MapFrom(c => c.Subjects))
                    .ForMember(c => c.Students, opt => opt.MapFrom(c => c.Students))
                    .ForMember(c => c.Instructors, opt => opt.MapFrom(c => c.Instructors));
        }
    }
}