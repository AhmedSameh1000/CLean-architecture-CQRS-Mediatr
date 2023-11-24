using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using System.Data;

namespace SchoolProject.Infrustructure.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(c => c.StudentId);
            builder.Property(c => c.Address).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(200);
            builder.Property(c => c.NameAr).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(200);
            builder.Property(c => c.NameEn).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(100);
            builder.Property(c => c.phone).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(100);

            builder.HasOne(c => c.Department).WithMany(c => c.Students).HasForeignKey(c => c.DID);
            builder.HasMany(c => c.Subjects).WithMany(c => c.Students).UsingEntity<StudentSubject>();
        }
    }
}