using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;
using FutScore.Infrastructure.Repositories;
using FutScore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FutScore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            
            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Cache Service
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();

            // Repositories
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<IStadiumRepository, StadiumRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
} 