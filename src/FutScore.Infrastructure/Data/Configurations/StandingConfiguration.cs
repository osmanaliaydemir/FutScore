using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutScore.Infrastructure.Data.Configurations
{
    public class StandingConfiguration : IEntityTypeConfiguration<Standing>
    {
        public void Configure(EntityTypeBuilder<Standing> builder)
        {
            builder.ToTable("Standings");

            builder.HasKey(s => new { s.SeasonId, s.TeamId });

            builder.Property(s => s.Points)
                .HasDefaultValue(0);

            builder.Property(s => s.Wins)
                .HasDefaultValue(0);

            builder.Property(s => s.Draws)
                .HasDefaultValue(0);

            builder.Property(s => s.Losses)
                .HasDefaultValue(0);

            builder.Property(s => s.GoalsFor)
                .HasDefaultValue(0);

            builder.Property(s => s.GoalsAgainst)
                .HasDefaultValue(0);

            // Relationships
            builder.HasOne(s => s.Season)
                .WithMany(s => s.Standings)
                .HasForeignKey(s => s.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Team)
                .WithMany(t => t.Standings)
                .HasForeignKey(s => s.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 