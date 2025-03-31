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

        // User related DbSets
        public DbSet<User>? Users { get; set; }
        public DbSet<UserRole>? UserRoles { get; set; }
        public DbSet<UserPermission>? UserPermissions { get; set; }
        public DbSet<UserSession>? UserSessions { get; set; }
        public DbSet<UserSettings>? UserSettings { get; set; }
        public DbSet<UserDevice>? UserDevices { get; set; }
        public DbSet<UserLocation>? UserLocations { get; set; }
        public DbSet<UserLanguage>? UserLanguages { get; set; }
        public DbSet<UserPreference>? UserPreferences { get; set; }
        public DbSet<UserToken>? UserTokens { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }
        public DbSet<UserVerification>? UserVerifications { get; set; }
        public DbSet<UserResetPassword>? UserResetPasswords { get; set; }
        public DbSet<UserAchievement>? UserAchievements { get; set; }
        public DbSet<UserProgress>? UserProgresses { get; set; }
        public DbSet<UserStatistics>? UserStatistics { get; set; }
        public DbSet<UserAnalytics>? UserAnalytics { get; set; }
        public DbSet<UserBlock>? UserBlocks { get; set; }
        public DbSet<UserSubscription>? UserSubscriptions { get; set; }
        public DbSet<UserPayment>? UserPayments { get; set; }

        // Match related DbSets
        public DbSet<Match>? Matches { get; set; }
        public DbSet<MatchEvent>? MatchEvents { get; set; }
        public DbSet<Team>? Teams { get; set; }
        public DbSet<Player>? Players { get; set; }
        public DbSet<League>? Leagues { get; set; }
        public DbSet<Prediction>? Predictions { get; set; }
        public DbSet<Friendship>? Friendships { get; set; }
        public DbSet<Role>? Roles { get; set; }

        // Statistics DbSets
        public DbSet<PlayerSeason>? PlayerSeasons { get; set; }
        public DbSet<TeamSeason>? TeamSeasons { get; set; }

        // System DbSets
        public DbSet<Configuration>? Configurations { get; set; }
        public DbSet<AuditLog>? AuditLogs { get; set; }
        public DbSet<SystemLog>? SystemLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Match relationships
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany()
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany()
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.League)
                .WithMany()
                .HasForeignKey(m => m.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Friendship relationships
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Requester)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(f => f.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Addressee)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(f => f.AddresseeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure UserBlock relationships
            modelBuilder.Entity<UserBlock>()
                .HasOne(ub => ub.Blocker)
                .WithMany(u => u.BlockedUsers)
                .HasForeignKey(ub => ub.BlockerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserBlock>()
                .HasOne(ub => ub.Blocked)
                .WithMany(u => u.BlockedByUsers)
                .HasForeignKey(ub => ub.BlockedId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure UserSettings relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Settings)
                .WithOne()
                .HasForeignKey<UserSettings>(us => us.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure TeamSeason relationships
            modelBuilder.Entity<TeamSeason>()
                .HasOne(ts => ts.Team)
                .WithMany()
                .HasForeignKey(ts => ts.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeamSeason>()
                .HasOne(ts => ts.League)
                .WithMany()
                .HasForeignKey(ts => ts.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeamSeason>()
                .HasOne(ts => ts.Season)
                .WithMany()
                .HasForeignKey(ts => ts.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

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
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
} 