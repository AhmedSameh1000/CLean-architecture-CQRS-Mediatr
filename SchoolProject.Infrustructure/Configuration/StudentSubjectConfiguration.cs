using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configuration
{
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(c => new { c.StuId, c.SubId });
            builder.HasOne(c => c.Student).WithMany(c => c.StudentSubjects).HasForeignKey(c => c.StuId);
            builder.HasOne(c => c.Subject).WithMany(c => c.StudentSubjects).HasForeignKey(c => c.SubId);
        }
    }
}