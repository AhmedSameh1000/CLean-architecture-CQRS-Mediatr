using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using System.Data;

namespace SchoolProject.Infrustructure.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(c => c.SubId);
            builder.Property(c => c.SubName).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50);
        }
    }
}