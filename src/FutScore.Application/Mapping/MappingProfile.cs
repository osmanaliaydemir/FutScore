using AutoMapper;
using FutScore.Application.DTOs;
using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Season;
using FutScore.Domain.Entities;

namespace FutScore.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // League Mappings
            CreateMap<CreateLeagueDto, League>();
            CreateMap<UpdateLeagueDto, League>();
            CreateMap<League, LeagueDto>();

            // Season Mappings
            CreateMap<CreateSeasonDto, Season>().ReverseMap();
            CreateMap<UpdateSeasonDto, Season>().ReverseMap();
            CreateMap<Season, SeasonDto>().ReverseMap();

            // Team Mappings
            //CreateMap<CreateTeamDto, Team>();
            //CreateMap<UpdateTeamDto, Team>();
            //CreateMap<Team, TeamDto>();

            //// Match Mappings
            //CreateMap<CreateMatchDto, Match>();
            //CreateMap<UpdateMatchDto, Match>();
            //CreateMap<Match, MatchDto>();

            //// Player Mappings
            //CreateMap<CreatePlayerDto, Player>();
            //CreateMap<UpdatePlayerDto, Player>();
            //CreateMap<Player, PlayerDto>();

            //// Stadium Mappings
            //CreateMap<CreateStadiumDto, Stadium>();
            //CreateMap<UpdateStadiumDto, Stadium>();
            //CreateMap<Stadium, StadiumDto>();
        }
    }
} 