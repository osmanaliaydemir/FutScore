using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutScore.Infrastructure.Data.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.MatchDate)
                .IsRequired();

            builder.Property(m => m.Venue)
                .HasMaxLength(100);

            builder.Property(m => m.Stadium)
                .HasMaxLength(100);

            builder.Property(m => m.MatchStatus)
                .HasMaxLength(50);

            builder.Property(m => m.MatchType)
                .HasMaxLength(50);

            builder.Property(m => m.Competition)
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(m => m.HomeTeam)
                .WithMany()
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.AwayTeam)
                .WithMany()
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.League)
                .WithMany()
                .HasForeignKey(m => m.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.MatchEvents)
                .WithOne(e => e.Match)
                .HasForeignKey(e => e.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Predictions)
                .WithOne(p => p.Match)
                .HasForeignKey(p => p.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
