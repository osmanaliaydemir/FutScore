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
using FutScore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FutScore.Application.Settings;
using FutScore.Infrastructure.Data;

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

// Add Infrastructure Services
builder.Services.AddInfrastructure(builder.Configuration);

// JWT Configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

// AutoMapper Configuration
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Service Registrations
builder.Services.AddScoped<IGenericService<TeamDto>, TeamService>();
builder.Services.AddScoped<IGenericService<MatchDto>, MatchService>();
builder.Services.AddScoped<IGenericService<PredictionDto>, PredictionService>();

// Specific Service Registrations
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IPredictionService, PredictionService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FutScore API V1");
    c.RoutePrefix = string.Empty; // Bu satır Swagger'ı root URL'de açacak
});

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
