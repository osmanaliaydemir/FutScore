using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutScore.Infrastructure.Data.Configurations
{
    public class UserBlockConfiguration : IEntityTypeConfiguration<UserBlock>
    {
        public void Configure(EntityTypeBuilder<UserBlock> builder)
        {
            builder.ToTable("UserBlocks");

            builder.HasKey(ub => ub.Id);

            builder.Property(ub => ub.Reason)
                .HasMaxLength(200);

            // Relationships
            builder.HasOne(ub => ub.Blocker)
                .WithMany(u => u.BlockedUsers)
                .HasForeignKey(ub => ub.BlockerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ub => ub.Blocked)
                .WithMany(u => u.BlockedByUsers)
                .HasForeignKey(ub => ub.BlockedId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 