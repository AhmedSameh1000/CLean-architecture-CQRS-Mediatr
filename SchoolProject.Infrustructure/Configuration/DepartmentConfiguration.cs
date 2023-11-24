using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using System.Data;

namespace SchoolProject.Infrustructure.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(c => c.DID);
            builder.Property(c => c.DNameAr).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(200);
            builder.Property(c => c.DNameEn).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(100);

            builder.HasOne(c => c.Instructor)
                .WithOne(c => c.DepartmentManged)
                .HasForeignKey<Department>(c => c.InstructorManger)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Subjects).WithMany(c => c.Departments).UsingEntity<DepartmentSubject>();
        }
    }
}