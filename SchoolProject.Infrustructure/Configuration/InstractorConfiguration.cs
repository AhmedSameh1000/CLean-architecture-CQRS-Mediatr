using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using System.Data;

namespace SchoolProject.Infrustructure.Configuration
{
    public class InstractorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasMany(c => c.Subjects).WithMany(c => c.Instructors).UsingEntity<InstructorSubject>();

            builder.Property(c => c.InstructorNameAr).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(100);
            builder.Property(c => c.InstructorNameEn).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(100);
            builder.Property(c => c.salary).HasColumnType("decimal(18,2)").HasMaxLength(100);
            builder.HasOne(c => c.Supervisor)
                .WithMany(c => c.SupervisedInstructors)
                .HasForeignKey(c => c.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Department)
                .WithMany(c => c.Instructors)
                .HasForeignKey(c => c.DID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}