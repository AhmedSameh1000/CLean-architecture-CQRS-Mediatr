using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty;

namespace SchoolProject.Core.Feature.Students.DTOs
{
    public class UpdateStudentDTO : IBaseIDDTO, IStudentProperty
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public string Address { get; set; }

        public string phone { get; set; }

        public int DepartmentId { get; set; }
    }
}