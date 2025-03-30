using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using FutScore.Application.Mapping;
using FutScore.Application.Interfaces;
using FutScore.Application.Services;
using Microsoft.Extensions.Configuration;

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

