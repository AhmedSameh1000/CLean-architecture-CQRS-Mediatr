namespace SchoolProject.Data.Entities
{
    public class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Instructors = new HashSet<Instructor>();
            Subjects = new HashSet<Subject>();
        }

        //[Key]
        public int DID { get; set; }

        //[StringLength(200)]
        public string DNameEn { get; set; }

        //[StringLength(300)]
        public string DNameAr { get; set; }

        public int? InstructorManger { get; set; }

        //[InverseProperty("DepartmentManged")]
        //[ForeignKey("InstructorManger")]
        public Instructor? Instructor { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        //[InverseProperty("Department")]
        public virtual ICollection<Instructor> Instructors { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}