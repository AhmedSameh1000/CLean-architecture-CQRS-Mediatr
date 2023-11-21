namespace SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty
{
    public class StudentProperty : IStudentProperty
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string phone { get; set; }

        public int DepartmentId { get; set; }
    }
}