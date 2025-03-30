using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutScore.Infrastructure.Data.Configurations
{
    public class TeamSeasonConfiguration : IEntityTypeConfiguration<TeamSeason>
    {
        public void Configure(EntityTypeBuilder<TeamSeason> builder)
        {
            builder.ToTable("TeamSeasons");

            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.Season)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(ts => ts.Form)
                .IsRequired();

            builder.Property(ts => ts.HomeForm)
                .IsRequired();

            builder.Property(ts => ts.AwayForm)
                .IsRequired();

            // Relationships
            builder.HasOne(ts => ts.Team)
                .WithMany()
                .HasForeignKey(ts => ts.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.League)
                .WithMany()
                .HasForeignKey(ts => ts.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.Season)
                .WithMany()
                .HasForeignKey(ts => ts.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 