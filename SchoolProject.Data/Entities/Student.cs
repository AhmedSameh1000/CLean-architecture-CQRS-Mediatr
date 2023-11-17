using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Student
    {
        public int StudentId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public string phone { get; set; }

        public int? DID { get; set; }

        [ForeignKey("DID")]
        public virtual Department Department { get; set; }

        //[InverseProperty("Students")]
    }
}