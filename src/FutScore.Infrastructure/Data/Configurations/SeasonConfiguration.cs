using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutScore.Infrastructure.Data.Configurations
{
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.ToTable("Seasons");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.SeasonName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.StartDate)
                .IsRequired();

            builder.Property(s => s.EndDate)
                .IsRequired();

            // Relationships
            builder.HasOne(s => s.League)
                .WithMany(l => l.Seasons)
                .HasForeignKey(s => s.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.SeasonTeams)
                .WithOne(st => st.Season)
                .HasForeignKey(st => st.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Matches)
                .WithOne(m => m.Season)
                .HasForeignKey(m => m.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Standings)
                .WithOne(st => st.Season)
                .HasForeignKey(st => st.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 