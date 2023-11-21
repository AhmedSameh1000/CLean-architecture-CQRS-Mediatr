using SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty;

namespace SchoolProject.Core.Feature.Students.DTOs
{
    public class CreateStudentDTO : IStudentProperty
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string phone { get; set; }

        public int DepartmentId { get; set; }
    }
}