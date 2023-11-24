namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        public int StuId { get; set; }

        public int SubId { get; set; }

        public decimal? StudentGrade { get; set; }

        //[ForeignKey("StuId")]
        public virtual Student Student { get; set; }

        //[ForeignKey("SubId")]
        public virtual Subject Subject { get; set; }
    }
}