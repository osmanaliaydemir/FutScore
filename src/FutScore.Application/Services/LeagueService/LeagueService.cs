using AutoMapper;
using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;
using FutScore.Application.DTOs.Team;
using FutScore.Domain;
using FutScore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FutScore.Application.Services.LeagueService
{
    public class LeagueService : ILeagueService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private Guid? _userId;
        public LeagueService(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        // CRUD Operations
        public async Task<LeagueDto> CreateLeagueAsync(LeagueCreateDto createDto)
        {
            var league = _mapper.Map<League>(createDto);
            league.CreatedAt = DateTime.UtcNow;
            league.CreatedBy = _userId; // TODO: Get from current user

            _context.Leagues.Add(league);
            await _context.SaveChangesAsync();

            return _mapper.Map<LeagueDto>(league);
        }

        public async Task<LeagueDto> UpdateLeagueAsync(Guid id, LeagueUpdateDto updateDto)
        {
            var league = await _context.Leagues.FindAsync(id);
            if (league == null)
                return null;

            _mapper.Map(updateDto, league);
            league.UpdatedAt = DateTime.UtcNow;
            league.UpdatedBy = _userId; // TODO: Get from current user

            await _context.SaveChangesAsync();
            return _mapper.Map<LeagueDto>(league);
        }

        public async Task<bool> DeleteLeagueAsync(Guid id)
        {
            var league = await _context.Leagues.FindAsync(id);
            if (league == null)
                return false;

            league.IsDeleted = true;
            league.DeletedAt = DateTime.UtcNow;
            league.DeletedBy = _userId; // TODO: Get from current user

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LeagueDto> GetLeagueByIdAsync(Guid id)
        {
            var league = await _context.Leagues.FindAsync(id);
            return _mapper.Map<LeagueDto>(league);
        }

        public async Task<IEnumerable<LeagueDto>> GetAllLeaguesAsync()
        {
            var leagues = await _context.Leagues.Where(l => !l.IsDeleted).OrderBy(l => l.Name).ToListAsync();
            return _mapper.Map<IEnumerable<LeagueDto>>(leagues);
        }

        // Additional Operations

        public async Task<List<LeagueListDto>> GetLeagueListAsync(LeagueFilterDto filter)
        {
            var query = _context.Leagues.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.Country))
                query = query.Where(l => l.Country == filter.Country);

            if (!string.IsNullOrEmpty(filter.Status))
                query = query.Where(l => l.MatchStatus == filter.Status);

            if (filter.IsActive.HasValue)
                query = query.Where(l => l.IsActive == filter.IsActive);

            var leagues = await query.ToListAsync();
            return _mapper.Map<List<LeagueListDto>>(leagues);
        }
        public async Task<List<LeagueDto>> GetLeaguesByCountryAsync(string country)
        {
            var leagues = await _context.Leagues
                .Where(l => l.Country == country && !l.IsDeleted)
                .OrderBy(l => l.Name)
                .ToListAsync();

            return _mapper.Map<List<LeagueDto>>(leagues);
        }

        public async Task<bool> UpdateLeagueStatusAsync(Guid leagueId, string status)
        {
            var league = await _context.Leagues.FindAsync(leagueId);
            if (league == null) return false;

            league.MatchStatus = status;
            league.UpdatedAt = DateTime.UtcNow;
            league.UpdatedBy = _userId; // TODO: Get from current user

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProcessResult> AddTeamToLeagueAsync(Guid leagueId, Guid teamId)
        {
            var processResult = new ProcessResult();
            var league = await _context.Leagues.Include(l => l.Teams).FirstOrDefaultAsync(l => l.Id == leagueId);
            var team = await _context.Teams.FindAsync(teamId);

            if (league == null || team == null)
            {
                processResult.Success = false;
                processResult.Message = "League or Team not found";
                return processResult;
            }

            if (!league.Teams.Any(t => t.Id == teamId))
            {
                league.Teams.Add(team);
                league.UpdatedAt = DateTime.UtcNow;
                league.UpdatedBy = _userId; // TODO: Get from current user

                var result = await _context.SaveChangesAsync();
                processResult.Success = result > 0;
                processResult.Message = processResult.Success ? "Team added successfully" : "Failed to add team";
            }
            else
            {
                processResult.Success = false;
                processResult.Message = "Team already exists in the league";
            }

            return processResult;
        }

        public async Task<bool> RemoveTeamFromLeagueAsync(Guid leagueId, Guid teamId)
        {
            var league = await _context.Leagues
                .Include(l => l.Teams)
                .FirstOrDefaultAsync(l => l.Id == leagueId);

            if (league == null) return false;

            var team = league.Teams.FirstOrDefault(t => t.Id == teamId);
            if (team != null)
            {
                league.Teams.Remove(team);
                league.UpdatedAt = DateTime.UtcNow;
                league.UpdatedBy = _userId; // TODO: Get from current user

                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<TeamDto>> GetLeagueTeamsAsync(Guid leagueId)
        {
            var teams = await _context.Teams
                .Where(t => t.LeagueId == leagueId && !t.IsDeleted)
                .OrderBy(t => t.Name)
                .ToListAsync();

            return _mapper.Map<List<TeamDto>>(teams);
        }

        public async Task<List<MatchDto>> GetLeagueMatchesAsync(Guid leagueId)
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.LeagueId == leagueId && !m.IsDeleted)
                .OrderByDescending(m => m.MatchDate)
                .ToListAsync();

            return _mapper.Map<List<MatchDto>>(matches);
        }

        public async Task<List<PlayerDto>> GetLeaguePlayersAsync(Guid leagueId)
        {
            var players = await _context.Players
                .Include(p => p.Team)
                .Where(p => p.Team.LeagueId == leagueId && !p.IsDeleted)
                .OrderBy(p => p.Team.Name)
                .ThenBy(p => p.Name)
                .ToListAsync();

            return _mapper.Map<List<PlayerDto>>(players);
        }

        public async Task<LeagueStatsDto> GetLeagueStatsAsync(Guid leagueId)
        {
            var league = await _context.Leagues
                .Include(l => l.Teams)
                .Include(l => l.Matches)
                .FirstOrDefaultAsync(l => l.Id == leagueId);

            if (league == null)
                return null;

            return new LeagueStatsDto
            {
                TotalTeams = league.Teams.Count,
                TotalMatches = league.Matches.Count,
                CompletedMatches = league.Matches.Count(m => m.MatchStatus == "Completed"),
                UpcomingMatches = league.Matches.Count(m => m.MatchStatus == "Scheduled"),
                TotalGoals = league.Matches.Sum(m => m.HomeTeamScore.Value + m.AwayTeamScore.Value),
                AverageGoalsPerMatch = league.Matches.Any()
                    ? (double)league.Matches.Sum(m => m.HomeTeamScore + m.AwayTeamScore) / league.Matches.Count
                    : 0
            };
        }

        private void SetUserIdAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var value = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (value is not null)
                _userId = Guid.Parse(value);
        }
    }
}
