using FutScore.Application.Interfaces;
using FutScore.Application.Mapping;
using FutScore.Application.Services.JwtService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FutScore.Application.Services;

namespace FutScore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IJwtService, JwtService>();

            // Application Services
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<ISeasonService, SeasonService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IStandingService, StandingService>();
            services.AddScoped<IPlayerService, PlayerService>();

            return services;
        }
    }
}

