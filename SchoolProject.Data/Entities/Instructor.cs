namespace SchoolProject.Data.Entities
{
    public class Instructor
    {
        public Instructor()
        {
            Subjects = new HashSet<Subject>();
            SupervisedInstructors = new HashSet<Instructor>();
        }

        public int Id { get; set; }
        public string InstructorNameAr { get; set; }
        public string InstructorNameEn { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public decimal salary { get; set; }

        //[InverseProperty("Instructor")]
        public Department DepartmentManged { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

        public int DID { get; set; }

        //[ForeignKey("DID")]
        //[InverseProperty("Instructors")]
        public Department Department { get; set; }

        public int? SupervisorId { get; set; }

        //[ForeignKey("SupervisorId")]
        //[InverseProperty("SupervisedInstructors")]
        public Instructor? Supervisor { get; set; }

        //[InverseProperty("Supervisor")]
        public ICollection<Instructor> SupervisedInstructors { get; set; }
    }
}