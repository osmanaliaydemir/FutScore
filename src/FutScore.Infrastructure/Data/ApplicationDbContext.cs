using FutScore.Domain;
using FutScore.Domain.Entities;
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
        public DbSet<Standing> Standings { get; set; }

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

            modelBuilder.Entity<Standing>()
                .HasKey(s => new { s.SeasonId, s.TeamId });

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

            // Configure Match relationships
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Season)
                .WithMany(s => s.Matches)
                .HasForeignKey(m => m.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.NoAction);

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

            // Configure Standing relationships
            modelBuilder.Entity<Standing>()
                .HasOne(s => s.Season)
                .WithMany(s => s.Standings)
                .HasForeignKey(s => s.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Standing>()
                .HasOne(s => s.Team)
                .WithMany(t => t.Standings)
                .HasForeignKey(s => s.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Player relationships
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
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