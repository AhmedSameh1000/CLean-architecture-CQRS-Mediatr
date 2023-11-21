using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Core.Feature.Students.Queries.Results
{
    public class StudentToReturn
    {
        public int StudentId { get; set; }

        [StringLength(200)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public string phone { get; set; }
        public string? DepartmentName { get; set; }
    }
}