using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data.Entities
{
    public class Subject
    {
        public Subject()
        {
            StudentSubjects = new HashSet<StudentSubject>();

            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }

        [Key]
        public int SubId { get; set; }

        [StringLength(500)]
        public string SubName { get; set; }

        public DateTime Period { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
    }
}