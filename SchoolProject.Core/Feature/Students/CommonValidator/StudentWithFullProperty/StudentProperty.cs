namespace SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty
{
    public class StudentProperty : IStudentProperty
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public string Address { get; set; }

        public string phone { get; set; }

        public int DepartmentId { get; set; }
    }
}