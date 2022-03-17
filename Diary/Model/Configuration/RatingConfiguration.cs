using Diary.Model.Domains;
using System.Data.Entity.ModelConfiguration;

namespace Diary.Model.Configuration
{
    public class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            ToTable("dbo.Ratings");
            HasKey(x => x.Id);
        }
    }
}
