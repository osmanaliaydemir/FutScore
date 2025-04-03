using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutScore.Infrastructure.Data.Configurations
{
    public class SeasonTeamConfiguration : IEntityTypeConfiguration<SeasonTeam>
    {
        public void Configure(EntityTypeBuilder<SeasonTeam> builder)
        {
            builder.ToTable("SeasonTeams");

            builder.HasKey(st => new { st.SeasonId, st.TeamId });

            // Relationships
            builder.HasOne(st => st.Season)
                .WithMany(s => s.SeasonTeams)
                .HasForeignKey(st => st.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(st => st.Team)
                .WithMany(t => t.SeasonTeams)
                .HasForeignKey(st => st.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 