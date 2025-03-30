using FutScore.Application.Interfaces;
using FutScore.Application.Mapping;
using FutScore.Application.Services.JwtService;
using FutScore.Application.Services.AdminUserService;
using FutScore.Application.Services.LeagueService;
using FutScore.Application.Services.RoleService;
using FutScore.Application.Services.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FutScore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAdminUserService, AdminUserService>();
            return services;
        }
    }
}

