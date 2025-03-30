using FutScore.Application.Interfaces;
using FutScore.Application.Mapping;
using FutScore.Application.Services.JwtService;
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
            return services;
        }
    }
}

