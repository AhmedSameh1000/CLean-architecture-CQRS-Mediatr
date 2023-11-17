using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        [Key]
        public int StudentSubId { get; set; }

        public int StuId { get; set; }

        public int SubId { get; set; }

        [ForeignKey("StuId")]
        public virtual Student Student { get; set; }

        [ForeignKey("SubId")]
        public virtual Subject Subject { get; set; }
    }
}