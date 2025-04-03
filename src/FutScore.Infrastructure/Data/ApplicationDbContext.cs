using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace FutScore.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // Match related DbSets
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configure relationships
            modelBuilder.Entity<League>()
                .HasMany(l => l.Seasons)
                .WithOne(s => s.League)
                .HasForeignKey(s => s.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<League>()
                .HasMany(l => l.Teams)
                .WithOne(t => t.League)
                .HasForeignKey(t => t.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Season>()
                .HasMany(s => s.Matches)
                .WithOne(m => m.Season)
                .HasForeignKey(m => m.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.HomeMatches)
                .WithOne(m => m.HomeTeam)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.AwayMatches)
                .WithOne(m => m.AwayTeam)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stadium>()
                .HasMany(s => s.Teams)
                .WithOne(t => t.Stadium)
                .HasForeignKey(t => t.StadiumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Stadium>()
                .HasMany(s => s.Matches)
                .WithOne(m => m.Stadium)
                .HasForeignKey(m => m.StadiumId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes
            modelBuilder.Entity<League>()
                .HasIndex(l => l.Name)
                .IsUnique();

            modelBuilder.Entity<Season>()
                .HasIndex(s => new { s.LeagueId, s.SeasonName })
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasIndex(t => new { t.LeagueId, t.Name })
                .IsUnique();

            modelBuilder.Entity<Player>()
                .HasIndex(p => new { p.TeamId, p.JerseyNumber })
                .IsUnique();

            modelBuilder.Entity<Stadium>()
                .HasIndex(s => new { s.Name, s.City })
                .IsUnique();

            // Configure all relationships to use Restrict delete behavior
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Configure Match relationships and Value Objects
            modelBuilder.Entity<Match>(entity =>
            {
                // Configure Score Value Object
                entity.OwnsOne(m => m.Score, score =>
                {
                    score.Property(s => s.HomeScore).HasColumnName("HomeTeamScore");
                    score.Property(s => s.AwayScore).HasColumnName("AwayTeamScore");
                });

                // Configure MatchTime Value Object
                entity.OwnsOne(m => m.MatchTime, time =>
                {
                    time.Property(t => t.MatchDate).HasColumnName("MatchDate");
                    time.Property(t => t.StartTime).HasColumnName("StartTime");
                    time.Property(t => t.EndTime).HasColumnName("EndTime");
                });
            });

            // Configure Team relationships
            modelBuilder.Entity<Team>()
                .HasIndex(t => t.Name)
                .IsUnique();

            // Configure Stadium relationships
            modelBuilder.Entity<Stadium>()
                .HasIndex(s => s.Name)
                .IsUnique();

            // Global query filter for soft delete
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var propertyAccess = Expression.Property(parameter, "IsDeleted");
                    var condition = Expression.Lambda(Expression.Not(propertyAccess), parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(condition);
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }
        }
    }
} 