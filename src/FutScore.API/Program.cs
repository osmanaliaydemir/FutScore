using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Prediction;
using FutScore.Application.DTOs.Team;
using FutScore.Application.DTOs.User;
using FutScore.Application.Services.GenericService;
using FutScore.Application.Services.LeagueService;
using FutScore.Application.Services.MatchService;
using FutScore.Application.Services.PredictionService;
using FutScore.Application.Services.TeamService;
using FutScore.Application.Services.UserService;
using FutScore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FutScore API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Database Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper Configuration
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Service Registrations
builder.Services.AddScoped<IGenericService<UserDto>, UserService>();
builder.Services.AddScoped<IGenericService<TeamDto>, TeamService>();
builder.Services.AddScoped<IGenericService<MatchDto>, MatchService>();
builder.Services.AddScoped<IGenericService<PredictionDto>, PredictionService>();

// Specific Service Registrations
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IPredictionService, PredictionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FutScore API V1");
    c.RoutePrefix = string.Empty; // Bu satır Swagger'ı root URL'de açacak
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
