﻿namespace SchoolProject.Data.Entities
{
    public class Student
    {
        public Student()
        {
            Subjects = new HashSet<Subject>();
            StudentSubjects = new HashSet<StudentSubject>();
        }

        public int StudentId { get; set; }

        //[StringLength(500)]
        public string Address { get; set; }

        //[StringLength(200)]
        public string NameEn { get; set; }

        //[StringLength(500)]
        public string NameAr { get; set; }

        public string phone { get; set; }

        public int? DID { get; set; }

        //[ForeignKey("DID")]
        public virtual Department Department { get; set; }

        public ICollection<Subject> Subjects { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
        //[InverseProperty("Students")]
    }
}