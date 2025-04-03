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
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);



            services.Scan(scan => scan
             .FromAssemblies(typeof(IBaseService).Assembly) // Buraya hedef assembly'yi ekle
             .AddClasses()
             .AsImplementedInterfaces()
             .WithScopedLifetime());


            return services;
        }
    }
}

