namespace SchoolProject.Data.Entities
{
    public class Subject
    {
        public Subject()
        {
            Instructors = new HashSet<Instructor>();
            Students = new HashSet<Student>();
            Departments = new HashSet<Department>();
            StudentSubjects = new HashSet<StudentSubject>();
        }

        //[Key]
        public int SubId { get; set; }

        //[StringLength(500)]
        public string SubName { get; set; }

        public decimal SubjectGrade { get; set; }

        public int? Period { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}