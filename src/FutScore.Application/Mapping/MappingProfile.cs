using AutoMapper;
using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.MatchEvent;
using FutScore.Application.DTOs.Player;
using FutScore.Application.DTOs.Prediction;
using FutScore.Application.DTOs.Role;
using FutScore.Application.DTOs.Team;
using FutScore.Application.DTOs.User;
using FutScore.Domain.Entities;

namespace FutScore.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap();

            // Team Mappings
            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.League.Name))
                .ReverseMap();
            CreateMap<Team, TeamDetailDto>()
                .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players))
                .ForMember(dest => dest.HomeMatches, opt => opt.MapFrom(src => src.HomeMatches))
                .ForMember(dest => dest.AwayMatches, opt => opt.MapFrom(src => src.AwayMatches))
                .ReverseMap();

            // Match Mappings
            CreateMap<Match, MatchDto>()
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.Name))
                .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.Name))
                .ForMember(dest => dest.LeagueName, opt => opt.MapFrom(src => src.League.Name))
                .ReverseMap();
            CreateMap<Match, MatchDetailDto>()
                .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.MatchEvents))
                .ForMember(dest => dest.Predictions, opt => opt.MapFrom(src => src.Predictions))
                .ReverseMap();

            // MatchEvent Mappings
            CreateMap<MatchEvent, MatchEventDto>()
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.Name))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name))
                .ReverseMap();

            // League Mappings
            CreateMap<League, LeagueDto>()
                .ForMember(dest => dest.TeamCount, opt => opt.MapFrom(src => src.Teams.Count))
                .ReverseMap();
            CreateMap<League, LeagueDetailDto>()
                .ForMember(dest => dest.Teams, opt => opt.MapFrom(src => src.Teams))
                .ForMember(dest => dest.Matches, opt => opt.MapFrom(src => src.Matches))
                .ForMember(dest => dest.Seasons, opt => opt.MapFrom(src => src.Seasons))
                .ReverseMap();
            CreateMap<LeagueCreateDto, League>();
            CreateMap<LeagueUpdateDto, League>();

            // TeamSeason Mappings
            CreateMap<TeamSeason, TeamSeasonDto>();
            //CreateMap<TeamSeasonCreateDto, TeamSeason>();
            //CreateMap<TeamSeasonUpdateDto, TeamSeason>();

            // Role Mappings
            CreateMap<Role, RoleDto>();
            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();

            // Player Mappings
            CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name))
                .ReverseMap();
            CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.MatchEvents, opt => opt.MapFrom(src => src.MatchEvents))
                .ReverseMap();

            // Prediction Mappings
            CreateMap<Prediction, PredictionDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.MatchInfo, opt => opt.MapFrom(src => 
                    $"{src.Match.HomeTeam.Name} vs {src.Match.AwayTeam.Name}"))
                .ReverseMap();
            CreateMap<Prediction, PredictionDetailDto>()
                .ForMember(dest => dest.Match, opt => opt.MapFrom(src => src.Match))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            // User Mappings
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<ResetPasswordDto, User>();

            // UserRole Mappings
            CreateMap<UserRole, UserRoleDto>();
        }
    }
} 