using Diary.Model.Domains;
using System.Data.Entity.ModelConfiguration;

namespace Diary.Model.Configuration
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("dbo.Students");
            HasKey(x => x.Id);

            Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
