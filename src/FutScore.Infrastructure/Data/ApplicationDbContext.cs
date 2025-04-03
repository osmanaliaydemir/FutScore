using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        public DbSet<SeasonTeam> SeasonTeams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure all relationships to use Restrict delete behavior
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Configure composite keys
            modelBuilder.Entity<SeasonTeam>()
                .HasKey(st => new { st.SeasonId, st.TeamId });

            // Configure League relationships
            modelBuilder.Entity<League>()
                .HasIndex(l => l.Name)
                .IsUnique();

            // Configure Season relationships
            modelBuilder.Entity<Season>()
                .HasOne(s => s.League)
                .WithMany(l => l.Seasons)
                .HasForeignKey(s => s.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);

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

                // Configure relationships
                entity.HasOne(m => m.Season)
                    .WithMany(s => s.Matches)
                    .HasForeignKey(m => m.SeasonId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.HomeTeam)
                    .WithMany()
                    .HasForeignKey(m => m.HomeTeamId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.AwayTeam)
                    .WithMany()
                    .HasForeignKey(m => m.AwayTeamId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Stadium)
                    .WithMany(s => s.Matches)
                    .HasForeignKey(m => m.StadiumId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Team relationships
            modelBuilder.Entity<Team>()
                .HasIndex(t => t.Name)
                .IsUnique();

            // Configure Player relationships
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Stadium relationships
            modelBuilder.Entity<Stadium>()
                .HasIndex(s => s.Name)
                .IsUnique();

            // Configure SeasonTeam relationships
            modelBuilder.Entity<SeasonTeam>()
                .HasOne(st => st.Season)
                .WithMany(s => s.SeasonTeams)
                .HasForeignKey(st => st.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SeasonTeam>()
                .HasOne(st => st.Team)
                .WithMany(t => t.SeasonTeams)
                .HasForeignKey(st => st.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Apply configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

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