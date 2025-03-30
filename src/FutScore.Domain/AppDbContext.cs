using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace FutScore.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // User related DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserVerification> UserVerifications { get; set; }
        public DbSet<UserResetPassword> UserResetPasswords { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }
        public DbSet<UserAnalytics> UserAnalytics { get; set; }
        public DbSet<UserReport> UserReports { get; set; }
        public DbSet<UserBlock> UserBlocks { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }

        // Match related DbSets
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Role> Roles { get; set; }

        // Statistics DbSets
        public DbSet<PlayerSeason> PlayerSeasons { get; set; }
        public DbSet<TeamSeason> TeamSeasons { get; set; }

        // System DbSets
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

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
