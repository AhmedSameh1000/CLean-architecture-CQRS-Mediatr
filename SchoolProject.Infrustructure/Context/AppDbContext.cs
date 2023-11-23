using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorSubjects> InstructorSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstructorSubjects>().HasKey(c => new { c.SubjectId, c.InstructorId });
            modelBuilder.Entity<DepartmentSubject>().HasKey(c => new { c.SubID, c.DID });
            modelBuilder.Entity<StudentSubject>().HasKey(c => new { c.StuId, c.SubId });

            modelBuilder.Entity<Instructor>().HasMany(c => c.Subjects).WithMany(c => c.Instructors).UsingEntity<InstructorSubjects>();
            modelBuilder.Entity<Department>().HasMany(c => c.Subjects).WithMany(c => c.Departments).UsingEntity<DepartmentSubject>();
            modelBuilder.Entity<Student>().HasMany(c => c.Subjects).WithMany(c => c.Students).UsingEntity<StudentSubject>();
        }
    }
}