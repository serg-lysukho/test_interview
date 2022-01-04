using InterviewApp.DAL.Entities.Watchlist;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewApp.DAL.EntityConfiguration.Watchlist
{
    public class WatchlistConfiguration : IEntityTypeConfiguration<WatchlistItem>
    {
        public void Configure(EntityTypeBuilder<WatchlistItem> builder)
        {
            builder.HasKey(w => new { w.FilmId, w.UserId });
            builder.Property(w => w.FilmId).HasMaxLength(25);
        }
    }
}
