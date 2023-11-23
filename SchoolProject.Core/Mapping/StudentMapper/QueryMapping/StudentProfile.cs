using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.CommonLocalizer;

namespace SchoolProject.Core.Mapping.StudentMapper
{
    public partial class StudentProfile
    {
        public void GetStudentQueryMapping()
        {
            CreateMap<Student, StudentToReturn>()
             .ForMember(dest => dest.Name, Options => Options.MapFrom(src => LocalizeEntities.GetLicalizedName(src.NameAr, src.NameEn)))
             .ForMember(des => des.DepartmentName, options => options.MapFrom(src => LocalizeEntities.GetLicalizedName(src.Department.DNameAr, src.Department.DNameEn)))
             .ReverseMap();
        }
    }
}