using FutScore.Application.Interfaces;
using FutScore.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Reflection;

namespace FutScore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            // Services
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<ISeasonService, SeasonService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IStadiumService, StadiumService>();

            return services;
        }
    }
}

